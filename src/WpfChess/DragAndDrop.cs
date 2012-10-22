using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using Chess;
using System.Windows.Controls;

namespace WpfChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region STEP1 WIRE TO DETECT THE MOUSE DRAG

        void Piece_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !IsDragging)
            {
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    // Start drag
                    StartDragInProcAdorner(sender, e);

                    //indicate squares piece can reach
                    SquaresCanReach(sender);
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void SquaresCanReach(object sender)
        {
            //try cast to PieceImage
            PieceImage pieceImage = sender as PieceImage;            
            //if cast return not null
            if (pieceImage != null)
            {
                //list squares piece can reach
                List<Square> squaresCanReach = board.CanReach(pieceImage.piece);
                
                //for each square at canvas
                foreach (var child in canvasDraw.Children)
                {
                    //try cast to PieceImage
                    Rectangle rectangleCanReach = child as Rectangle;
                    //if cast return not null
                    if (rectangleCanReach != null)
                    {
                        //verify it is reachable
                        if (squaresCanReach.Exists(delegate(Square sq) { return sq.ToString().Equals(rectangleCanReach.Tag); }))
                        {
                            //make it visible
                            rectangleCanReach.Visibility = Visibility.Visible;
                        }
                    }

                    //try cast to Square
                    Image squareCanReach = child as Image;
                    //if cast return not null
                    if (squareCanReach != null)
                    {
                        //verify it is reachable
                        if (squaresCanReach.Exists(delegate(Square sq) { return sq.ToString().Equals(squareCanReach.Tag); }))
                        {
                            //set drop property
                            squareCanReach.AllowDrop = true;
                        }
                    }
                }
            }
        }

        void Piece_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }
        #endregion

        #region STEP2  BASIC Custom Cursor

        void Piece_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
            e.Handled = true;
        }
        
        #endregion
        
        #region STEP3 ADORNERS
        // 
        DragAdorner _adorner = null;
        AdornerLayer _layer;

        #endregion
        
        #region STEP5  Use DRAGOVER as a workaround

        FrameworkElement _dragScope;
        public FrameworkElement DragScope
        {
            get { return _dragScope; }
            set { _dragScope = value; }
        }

        private void StartDragInProcAdorner(object sender, MouseEventArgs e)
        {
            // Let's define our DragScope .. In this case it is every thing inside our main window .. 
            DragScope = Application.Current.MainWindow.Content as FrameworkElement;
            System.Diagnostics.Debug.Assert(DragScope != null);

            // We enable Drag & Drop in our scope ...  We are not implementing Drop, so it is OK, but this allows us to get DragOver 
            bool previousDrop = DragScope.AllowDrop;
            DragScope.AllowDrop = true;

            // Let's wire our usual events.. 
            // GiveFeedback just tells it to use no standard cursors..  

            GiveFeedbackEventHandler feedbackhandler = new GiveFeedbackEventHandler(Piece_GiveFeedback);
            ((UIElement)sender).GiveFeedback += feedbackhandler;

            // The DragOver event ... 
            DragEventHandler draghandler = new DragEventHandler(MainWindow_DragOver);
            DragScope.PreviewDragOver += draghandler;

            // Drag Leave is optional, but write up explains why I like it .. 
            DragEventHandler dragleavehandler = new DragEventHandler(DragScope_DragLeave);
            DragScope.DragLeave += dragleavehandler;

            // QueryContinue Drag goes with drag leave... 
            QueryContinueDragEventHandler queryhandler = new QueryContinueDragEventHandler(DragScope_QueryContinueDrag);
            DragScope.QueryContinueDrag += queryhandler;

            //Here we create our adorner.. 
            _adorner = new DragAdorner(DragScope, (UIElement)sender, true, 0.8);
            _layer = AdornerLayer.GetAdornerLayer(DragScope as Visual);
            _layer.Add(_adorner);

            IsDragging = true;
            _dragHasLeftScope = false;
            //Finally lets drag drop 
            DataObject data = new DataObject(System.Windows.DataFormats.Text.ToString(), "abcd");
            DragDropEffects de = DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Move);

            // Clean up our mess :) 
            DragScope.AllowDrop = previousDrop;
            AdornerLayer.GetAdornerLayer(DragScope).Remove(_adorner);
            _adorner = null;

            ((UIElement)sender).GiveFeedback -= feedbackhandler;
            DragScope.DragLeave -= dragleavehandler;
            DragScope.QueryContinueDrag -= queryhandler;
            DragScope.PreviewDragOver -= draghandler;

            IsDragging = false;
        }

        private bool _dragHasLeftScope = false;
        void DragScope_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (this._dragHasLeftScope)
            {
                e.Action = DragAction.Cancel;
                e.Handled = true;
            }
        }

        void DragScope_DragLeave(object sender, DragEventArgs e)
        {
            if (e.OriginalSource == DragScope)
            {
                Point p = e.GetPosition(DragScope);
                Rect r = VisualTreeHelper.GetContentBounds(DragScope);
                if (!r.Contains(p))
                {
                    this._dragHasLeftScope = true;
                    e.Handled = true;
                }
            }
        }

        void MainWindow_DragOver(object sender, DragEventArgs args)
        {
            if (_adorner != null)
            {
                _adorner.LeftOffset = args.GetPosition(DragScope).X /* - _startPoint.X */ ;
                _adorner.TopOffset = args.GetPosition(DragScope).Y /* - _startPoint.Y */ ;
            }
        }

        #endregion
        
        #region Drop Events

        void Square_Drop(object sender, DragEventArgs e)
        {
            IDataObject data = e.Data;

            if (data.GetDataPresent(DataFormats.Text))
            {
                MessageBox.Show(
                    string.Format("right format, thanks for dropping '{0}'",
                    ((string)data.GetData(DataFormats.Text))));
            }
        }

        void Square_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("Images"))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        void Square_DragLeave(object sender, DragEventArgs e)
        {

        }

        void Square_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("Image"))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        #endregion

        private Point _startPoint;
        private bool _isDragging;

        public bool IsDragging
        {
            get { return _isDragging; }
            set { _isDragging = value; }
        } 
    }
}

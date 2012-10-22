using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chess;
using System.Windows.Threading;
using System.Windows.Resources;
using System.IO;
using WpfChess.DragDropFrameworkData;
using WpfChess.DragDropFramework;


namespace WpfChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //
        Board board;

        // When changing the value of this constant, you have to change the values of
        // the imageList.ImageSize.Height and Width with the same.
        private const int PIECE_WIDTH = 27;
        private const int SQUARE_WIDTH = 76;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            //Fluxo principal:
            //1. Jogador solicita ao sistema um novo jogo, informando a versão, seu ID e o ID do seu oponente
            //2. Sistema valida a versão informada contra sua própria
            //3. Sistema verifica que o oponente ainda não iniciou o jogo
            //4. Sistema informa que as peças do jogador serão as brancas
            //5. Sistema inicia um novo objeto board e entra em modo de aguardando segundo jogador
            
            //create board object
            board = new Board();
            //create board presentation
            CreateBoard();

            //6. Sistema informa jogador que está em aguardo e gera uma cópia do objeto board no client
            //7. Sistema informa jogador que o oponente aceitou o jogo.
            //8. Jogador realiza a jogada.
            //9. Sistema valida a jogada e informa o oponente sobre a jogada.
            //10. Retorna para o passo 8 do fluxo principal.

            //Fluxo alternativo 1:
            //Este fluxo se inicia após o passo 2 do fluxo principal
            //1. Sistema informa que a versão do sistema é incompatível
            //2. Caso de uso é finalizado

            //Fluxo alternativo 2:
            //Este fluxo se inicia após o passo 3 do fluxo principal
            //1. Sistema informa que o oponente já iniciou o jogo
            //2. Sistema informa que as peças do jogador serão as pretas
            //3. Sistema informa o oponente que o jogo já pode ser iniciado
            //4. Sistema informa o jogador que a jogada inicial foi realizada
            //5. Retorna para o passo 8 do fluxo principal.

            //Fluxo alternativo 3:
            //Esse fluxo se inicia após o passo 8 do fluxo principal
            //1. Sistema identifica que a jogada produz cheque-mate.
            //2. Sistema finaliza o jogo.
        }

        #region Board Create Methods

        private void CreateBoard()
        {            
            //create board object
            board = new Board();

            #region Canvas drag methods
            // Data Providers/Consumers
            CanvasDataProvider<Canvas, PieceImage> canvasPieceImageDataProvider =
                new CanvasDataProvider<Canvas, PieceImage>("CanvasPieceImageObject");

            CanvasDataConsumer<Canvas, PieceImage> canvaspieceImageDataConsumer =
                new CanvasDataConsumer<Canvas, PieceImage>(new string[] { "CanvaspieceImageObject" });

            // Drag Managers
            DragManager dragHelperCanvasDraw = new DragManager(this.canvasDraw,
                new IDataProvider[] {
                     canvasPieceImageDataProvider,
                });

            //// Drop Managers
            //DropManager dropHelperCanvas0 = new DropManager(this.canvasDraw,
            //    new IDataConsumer[] {
            //        canvasPieceImageDataConsumer,
            //    });
            #endregion

            //for each square...
            foreach (Square square in board.SquareList)
            {
                //create square
                CreateSquare(square);
            }
            //set main window new size using canvas size to determine client area size
            this.Width = canvasDraw.Width + 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            this.Height = canvasDraw.Height + SystemParameters.CaptionHeight + 2 * SystemParameters.ResizeFrameHorizontalBorderHeight;
            //center screen main window
            this.Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;
            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="square"></param>
        private void CreateSquare(Square square)
        {
            SquareImage newSquare = null;

            //verify square color and get correct image
            if (square.color == Square.Color.Dark)
            {
                newSquare = CreateSquareImageFromResource("Images/darkSquare.bmp", (double)SQUARE_WIDTH);
                //set dark square opacity to control its darkness using white background as mix color
                newSquare.SetValue(Image.OpacityProperty, 0.8);
            }
            else
            {
                newSquare = CreateSquareImageFromResource("Images/lightSquare.bmp", (double)SQUARE_WIDTH);
            }            

            //set tile tag name
            newSquare.Tag = square.ToString();
            //add image object with tile image to canvas object control collection
            //canvasDraw.Children.Add(newSquare);
            //set tile postion
            //newSquare.SetValue(Canvas.LeftProperty, (double)square.X * SQUARE_WIDTH - SQUARE_WIDTH/2);
            //newSquare.SetValue(Canvas.TopProperty, (double)square.Y * SQUARE_WIDTH - SQUARE_WIDTH/2);

            //create a canvas
            Canvas newCanvas = new Canvas();
            //set canvas tag name
            newCanvas.Tag = square.ToString();
            //add new canvas object to canvas global object control collection
            canvasDraw.Children.Add(newCanvas);
            //set rectangle dimensions
            newCanvas.Width = (double)SQUARE_WIDTH;
            newCanvas.Height = (double)SQUARE_WIDTH;
            //set rectangle position
            newCanvas.SetValue(Canvas.LeftProperty, (double)square.X * SQUARE_WIDTH - SQUARE_WIDTH / 2);
            newCanvas.SetValue(Canvas.TopProperty, (double)square.Y * SQUARE_WIDTH - SQUARE_WIDTH / 2);

            //create a rectangle shape
            Rectangle newRectangle = new Rectangle();
            //set rectangle tag name
            newRectangle.Tag = square.ToString();
            //set rectangle properties
            newRectangle.Fill = Brushes.Red;
            newRectangle.Stroke = null;
            newRectangle.Opacity = .5;
            newRectangle.Visibility = Visibility.Hidden;
            //add rectangle object to canvas object control collection
            //canvasDraw.Children.Add(newRectangle);
            //set rectangle dimensions
            newRectangle.Width = (double)SQUARE_WIDTH;
            newRectangle.Height = (double)SQUARE_WIDTH;
            ////set rectangle position
            //newRectangle.SetValue(Canvas.LeftProperty, (double)square.X * SQUARE_WIDTH - SQUARE_WIDTH / 2);
            //newRectangle.SetValue(Canvas.TopProperty, (double)square.Y * SQUARE_WIDTH - SQUARE_WIDTH / 2);

            newCanvas.Children.Add(newSquare);
            newCanvas.Children.Add(newRectangle);

            //set canvas new size
            canvasDraw.Width = (double)square.X * SQUARE_WIDTH + SQUARE_WIDTH;
            canvasDraw.Height = (double)square.Y * SQUARE_WIDTH + SQUARE_WIDTH;

            //create label beside board
            CreateLabel(square);

            //create piece attached to canvas square
            CreatePiece(square.Piece, newCanvas);

            #region Canvas drag methods

            // Data Providers/Consumers
            CanvasDataProvider<Canvas, PieceImage> canvasPieceImageDataProvider =
                new CanvasDataProvider<Canvas, PieceImage>("CanvasPieceImageObject");

            CanvasDataConsumer<Canvas, SquareImage> canvasSquareImageDataConsumer =
                new CanvasDataConsumer<Canvas, SquareImage>(new string[] { "CanvasSquareImageObject" });

            // Drag Managers
            DragManager dragHelperCanvasSquare = new DragManager(newCanvas,
                new IDataProvider[] {
                     canvasPieceImageDataProvider,
                });

            // Drop Managers
            DropManager dropHelperCanvasSquare = new DropManager(newCanvas,
                new IDataConsumer[] {
                    canvasSquareImageDataConsumer,
                });

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="piece"></param>
        private void CreatePiece(Piece piece,Canvas canvas)
        {
            //verify if there is a piece to create
            if (piece == null) return;

            // Create Image Element
            PieceImage newPiece = null;

            //choose image source by piece type
            switch (piece.GetType().Name)
            {
                case "Pawn":
                    if (piece.Color == Chess.Piece.EColor.Black)
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_12.png", (double)PIECE_WIDTH);
                    else
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_06.png", (double)PIECE_WIDTH);
                    break;
                case "Rook":
                    if (piece.Color == Chess.Piece.EColor.Black)
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_11.png", (double)PIECE_WIDTH);
                    else
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_05.png", (double)PIECE_WIDTH);
                    break;
                case "Knight":
                    if (piece.Color == Chess.Piece.EColor.Black)
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_10.png", (double)PIECE_WIDTH);
                    else
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_04.png", (double)PIECE_WIDTH);
                    break;
                case "Bishop":
                    if (piece.Color == Chess.Piece.EColor.Black)
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_09.png", (double)PIECE_WIDTH);
                    else
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_03.png", (double)PIECE_WIDTH);
                    break;
                case "Queen":
                    if (piece.Color == Chess.Piece.EColor.Black)
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_08.png", (double)PIECE_WIDTH);
                    else
                        newPiece = (PieceImage)CreatePieceImageFromResource("Images/wooden-chess-pieces_02.png", (double)PIECE_WIDTH);
                    break;
                case "King":
                    if (piece.Color == Chess.Piece.EColor.Black)
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_07.png", (double)PIECE_WIDTH);
                    else
                        newPiece = CreatePieceImageFromResource("Images/wooden-chess-pieces_01.png", (double)PIECE_WIDTH);
                    break;
            }
            
            //set image piece atribute
            newPiece.piece = piece;
            //add image object with tile image to canvas object control collection
            canvas.Children.Add(newPiece);
            //piece dimensions difference
            double diffXPieceTile = (SQUARE_WIDTH - PIECE_WIDTH) / 2;
            double diffYPieceTile = (SQUARE_WIDTH - newPiece.Source.Height) / 2;
            //set piece position
            //newPiece.SetValue(Canvas.LeftProperty, (double)piece.square.X * SQUARE_WIDTH - SQUARE_WIDTH/2 + diffXPieceTile);
            //newPiece.SetValue(Canvas.TopProperty, (double)piece.square.Y * SQUARE_WIDTH - SQUARE_WIDTH/2 + diffYPieceTile);
            newPiece.SetValue(Canvas.LeftProperty, diffXPieceTile);
            newPiece.SetValue(Canvas.TopProperty, diffYPieceTile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="square"></param>
        private void CreateLabel(Square square)
        {
            //verify if it's number row
            if (square.ToString().Substring(0,1).Equals("A"))
            {
                //create number label 
                Label newLabel = new Label();
                //set label visual properties
                newLabel.Height = double.NaN;
                newLabel.Width = double.NaN;
                newLabel.FontWeight = FontWeights.Bold;
                newLabel.FontSize = 20;
                newLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                newLabel.VerticalContentAlignment = VerticalAlignment.Center;
                newLabel.Padding = new Thickness(0);
                newLabel.Content = square.ToString().Substring(1,1);
                //add lable object to canvas object control collection
                canvasDraw.Children.Add(newLabel);
                //label dimensions difference
                newLabel.UpdateLayout();
                double diffXLabel = (SQUARE_WIDTH / 2 - newLabel.ActualWidth) / 2 - newLabel.ActualWidth / 2;
                double diffYLabel = (SQUARE_WIDTH/2  - newLabel.ActualHeight) / 2;
                //set tile postion
                newLabel.SetValue(Canvas.LeftProperty, (double)square.X * SQUARE_WIDTH - diffXLabel);
                newLabel.SetValue(Canvas.TopProperty, diffYLabel);
            }
            
            //verify if it's number row
            if(square.ToString().Substring(1,1).Equals("1"))
            {
                //create letter label  
                Label newLabel = new Label();
                //set label visual properties
                newLabel.Height = double.NaN;
                newLabel.Width = double.NaN;
                newLabel.FontWeight = FontWeights.Bold;
                newLabel.FontSize = 20;
                newLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                newLabel.VerticalContentAlignment = VerticalAlignment.Center;
                newLabel.Padding = new Thickness(0);
                newLabel.Content = square.ToString().Substring(0,1);
                //add lable object to canvas object control collection
                canvasDraw.Children.Add(newLabel);
                //label dimensions difference
                newLabel.UpdateLayout();
                double diffXLabel = (SQUARE_WIDTH/2 - newLabel.ActualWidth )/ 2;
                double diffYLabel = (SQUARE_WIDTH / 2 - newLabel.ActualHeight) / 2 - newLabel.ActualHeight / 2;
                //set tile postion
                newLabel.SetValue(Canvas.LeftProperty, diffXLabel);
                newLabel.SetValue(Canvas.TopProperty, (double)square.Y * SQUARE_WIDTH + diffYLabel * 3 / 2);
            }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        private PieceImage CreatePieceImageFromResource(string resourcePath, double finalWidth)
        {
            // Create Image Element
            PieceImage newImage = new PieceImage();
            newImage.Width = finalWidth;

            // Create source
            BitmapImage newBitmapImage = new BitmapImage();

            // BitmapImage.UriSource must be in a BeginInit/EndInit block
            newBitmapImage.BeginInit();

            //set image stream from application resource using path to resource           
            newBitmapImage.StreamSource = Application.GetResourceStream(new Uri(resourcePath, UriKind.Relative)).Stream;

            // To save significant application memory, set the DecodePixelWidth or  
            // DecodePixelHeight of the BitmapImage value of the image source to the desired 
            // height or width of the rendered image. If you don't do this, the application will 
            // cache the image as though it were rendered as its normal size rather then just 
            // the size that is displayed.
            // Note: In order to preserve aspect ratio, set DecodePixelWidth
            // or DecodePixelHeight but not both.
            newBitmapImage.DecodePixelWidth = (int)finalWidth;
            
            // BitmapImage.UriSource must be in a BeginInit/EndInit block
            newBitmapImage.EndInit();
            
            //set image source
            newImage.Source = newBitmapImage;

            //return image object
            return newImage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        private SquareImage CreateSquareImageFromResource(string resourcePath, double finalWidth)
        {
            // Create Image Element
            SquareImage newImage = new SquareImage();
            newImage.Width = finalWidth;

            // Create source
            BitmapImage newBitmapImage = new BitmapImage();

            // BitmapImage.UriSource must be in a BeginInit/EndInit block
            newBitmapImage.BeginInit();

            //set image stream from application resource using path to resource           
            newBitmapImage.StreamSource = Application.GetResourceStream(new Uri(resourcePath, UriKind.Relative)).Stream;

            // To save significant application memory, set the DecodePixelWidth or  
            // DecodePixelHeight of the BitmapImage value of the image source to the desired 
            // height or width of the rendered image. If you don't do this, the application will 
            // cache the image as though it were rendered as its normal size rather then just 
            // the size that is displayed.
            // Note: In order to preserve aspect ratio, set DecodePixelWidth
            // or DecodePixelHeight but not both.
            newBitmapImage.DecodePixelWidth = (int)finalWidth;

            // BitmapImage.UriSource must be in a BeginInit/EndInit block
            newBitmapImage.EndInit();

            //set image source
            newImage.Source = newBitmapImage;

            //return image object
            return newImage;
        }

        #endregion
    }
}

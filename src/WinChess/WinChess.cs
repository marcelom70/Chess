using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chess;

namespace WinChess
{
    public partial class WinChess : Form
    {

        //
        private bool dragInProgress = false;
        //
        int MouseDownX = 0;
        int MouseDownY = 0;
        //
        
        Board board;

        Piece.EColor myColor;

        List<PictureBox> pictureList = new List<PictureBox>();

        // When changing the value of this constant, you have to change the values of
        // the imageList.ImageSize.Height and Width with the same.
        private const int PIC_LEN = 50;

        public WinChess()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Creates a chess board in the for, and two sets of pieces in Staunton pattern

            btnStart.Enabled = false;
            //Fluxo principal:
            //1. Jogador solicita ao sistema um novo jogo, informando a versão, seu ID e o ID do seu oponente
            //2. Sistema valida a versão informada contra sua própria
            //3. Sistema verifica que o oponente ainda não iniciou o jogo
            //4. Sistema informa que as peças do jogador serão as brancas
            //5. Sistema inicia um novo objeto board e entra em modo de aguardando segundo jogador
            board = new Board();

            myColor = Piece.EColor.White;

		    // Get a Graphics object from the form's handle.
            //Graphics graphics = Graphics.FromHwnd(this.Handle);

            //foreach (Square square in board.squareList)
            //{
            //    imlChess.Draw(graphics, new Point(square.X * PIC_LEN, square.Y * PIC_LEN), (int)square.color * 13);
            //    // Call Application.DoEvents to force a repaint of the form.
            //    Application.DoEvents();
            //}

            for (int i = 0; i < 2; i++)
            {
                List<Piece> set = null;

                if (i == 0)
                {
                    set = board.WhiteSet;
                }
                else
                {
                    set = board.BlackSet;
                }

                foreach (Piece piece in set)
                {
                    PictureBox pic = new PictureBox();
                    pic.Height = PIC_LEN;
                    pic.Width = PIC_LEN;

                    // The sequence of the images in the list must be strictly this way:
                    // 1 - Pawn
                    // 2 - Knight
                    // 3 - Bishop
                    // 4 - Rook
                    // 5 - Queen
                    // 6 - King
                    // For black pieces, the total number of kinds of pieces is incremented to obtain
                    // the corresponding images.
                    pic.Image = imlChess.Images[piece.MaterialValue + (((int)piece.Color) * 6) + 1];

                    this.Controls.Add(pic);
                    pic.Top = piece.square.Y * PIC_LEN;
                    pic.Left = piece.square.X * PIC_LEN;
                    pic.Tag = piece.Color.ToString() + piece.ToString();

                    pic.Visible = true;

                    if (myColor != piece.Color)
                    {
                        pic.Enabled = false;
                    }
                    //else
                    {
                        pic.MouseDown += new MouseEventHandler(Piece_MouseDown);
                        pic.MouseUp += new MouseEventHandler(Piece_MouseUp);
                        pic.MouseMove += new MouseEventHandler(Piece_MouseMove);
                    }
                    pic.BringToFront();
                    pictureList.Add(pic);
                }
            }
        }

        private void WinChess_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event responsible for identifying that the user chose that specific picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Piece_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.dragInProgress)
            {
                this.dragInProgress = true;
                this.MouseDownX = e.X;
                this.MouseDownY = e.Y;
            }
            return;
        }

        /// <summary>
        /// Event responsible for detecting that the user dropped
        /// the picture upon the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Piece_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.dragInProgress = false;

                Square dropSquare = null;

                // Try to identify a square
                foreach (Square square in board.SquareList)
                {
                    if (((square.X * PIC_LEN) <= ((PictureBox)sender).Left & ((PictureBox)sender).Left < ((square.X * PIC_LEN) + PIC_LEN))
                        & ((square.Y * PIC_LEN) <= ((PictureBox)sender).Top & ((PictureBox)sender).Top < ((square.Y * PIC_LEN) + PIC_LEN)))
                    {
                        // Catches a pointer to the selected square and exits
                        dropSquare = square;
                        break;
                    }
                }

                Piece piece = null;

                // Find the piece in the corresponding collection, 
                // according the players pieces color
                string piecePrefix = ((PictureBox)sender).Tag.ToString();
                piecePrefix = piecePrefix.Substring(5, piecePrefix.Length - 5);
                if (board.Turn == Piece.EColor.White)
                {
                    piece = board.WhiteSet.Find(o => o.ToString() == piecePrefix);
                }
                else
                {
                    piece = board.BlackSet.Find(o => o.ToString() == piecePrefix);
                }

                // If the pointer is not set to null, this means the piece was correctly
                // put upon an existing square
                if (dropSquare != null)
                {
                    // Identify a castling
                    if (piece.GetType() == typeof(Rook) &&
                        (dropSquare.Piece != null && (dropSquare.Piece.Color == piece.Color &
                        dropSquare.Piece.GetType() == typeof(King))))
                    {
                        King king = (King)dropSquare.Piece;
                        if (board.Castling(king, (Rook)piece))
                        {
                            // Places the pictures in their respective squares
                            ((PictureBox)sender).Left = piece.square.X * PIC_LEN;
                            ((PictureBox)sender).Top = piece.square.Y * PIC_LEN;

                            // Find the king picture...
                            PictureBox picKing = pictureList.Find(p => p.Tag.ToString() == king.Color.ToString() + king.ToString());

                            picKing.Left = king.square.X * PIC_LEN;
                            picKing.Top = king.square.Y * PIC_LEN;

                            myColor = board.Turn;

                            foreach (PictureBox pic in pictureList)
                            {
                                pic.Enabled = !pic.Enabled; // (pic.Tag.ToString().Substring(0, 5) == myColor.ToString());
                            }

                        }
                        else
                        {
                            // Caution: code replication
                            int incrementX = 1;
                            int incrementY = 1;

                            if (((PictureBox)sender).Left > (piece.square.X * PIC_LEN))
                            {
                                incrementX *= -1;
                            }

                            // Moves the piece back to its original square
                            for (int X = ((PictureBox)sender).Left; X != (piece.square.X * PIC_LEN); X += (1 * incrementX))
                            {
                                ((PictureBox)sender).Left = X;
                            }

                            if (((PictureBox)sender).Top > (piece.square.Y * PIC_LEN))
                            {
                                incrementY *= -1;
                            }

                            // Moves the piece back to its original square
                            for (int Y = ((PictureBox)sender).Top; Y != (piece.square.Y * PIC_LEN); Y += (1 * incrementY))
                            {
                                ((PictureBox)sender).Top = Y;
                            }
                        }
                    }
                    else
                    { 
                        // If it matches a square, then submit to its constraints
                        if (board.Movement(piece, dropSquare))
                        {
                            // Places the piece on the square
                            //????piece.square = dropSquare;
                            //????dropSquare.Piece = piece;
                            board.Move(piece, dropSquare);
                            ((PictureBox)sender).Left = dropSquare.X * PIC_LEN;
                            ((PictureBox)sender).Top = dropSquare.Y * PIC_LEN;

                            myColor = board.Turn;

                            foreach (PictureBox pic in pictureList)
                            {
                                pic.Enabled = !pic.Enabled; // (pic.Tag.ToString().Substring(0, 5) == myColor.ToString());

                                if (pic.Tag.ToString().Substring(0, 5) == board.Turn.ToString())
                                {
                                    Piece p = null;

                                    string pPrefix = pic.Tag.ToString();
                                    pPrefix = pPrefix.Substring(5, pPrefix.Length - 5);
                                    if (board.Turn == Piece.EColor.White)
                                    {
                                        p = board.WhiteSet.Find(o => o.ToString() == pPrefix);
                                    }
                                    else
                                    {
                                        p = board.BlackSet.Find(o => o.ToString() == pPrefix);
                                    }

                                    if (p.square == null)
                                    {
                                        pic.Top = 0;
                                        pic.Left = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Caution: code replication
                            int incrementX = 1;
                            int incrementY = 1;

                            if (((PictureBox)sender).Left > (piece.square.X * PIC_LEN))
                            {
                                incrementX *= -1;
                            }

                            // Moves the piece back to its original square
                            for (int X = ((PictureBox)sender).Left; X != (piece.square.X * PIC_LEN); X += (1 * incrementX))
                            {
                                ((PictureBox)sender).Left = X;
                            }

                            if (((PictureBox)sender).Top > (piece.square.Y * PIC_LEN))
                            {
                                incrementY *= -1;
                            }

                            // Moves the piece back to its original square
                            for (int Y = ((PictureBox)sender).Top; Y != (piece.square.Y * PIC_LEN); Y += (1 * incrementY))
                            {
                                ((PictureBox)sender).Top = Y;
                            }
                        }
                    }
                }
                // Elsewhere in (or out!) the form, 
                // when it moves the piece back to its original square
                else
                {
                    // Caution: code replication
                    int incrementX = 1;
                    int incrementY = 1;

                    if (((PictureBox)sender).Left > (piece.square.X * PIC_LEN))
                    {
                        incrementX *= -1;
                    }

                    // Moves the piece back to its original square
                    for (int X = ((PictureBox)sender).Left; X != (piece.square.X * PIC_LEN); X += (1 * incrementX))
                    {
                        ((PictureBox)sender).Left = X;
                    }

                    if (((PictureBox)sender).Top > (piece.square.Y * PIC_LEN))
                    {
                        incrementY *= -1;
                    }

                    // Moves the piece back to its original square
                    for (int Y = ((PictureBox)sender).Top; Y != (piece.square.Y * PIC_LEN); Y += (1 * incrementY))
                    {
                        ((PictureBox)sender).Top = Y;
                    }
                }

            }

            if (board.Check)
            {
                MessageBox.Show("Cheque!", "Cheque");
            }
            return;
        }

        /// <summary>
        /// Event responsible for detecting the movement of mouse cursor
        /// and make the picture follow it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Piece_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragInProgress)
            {
                Point temp = new Point();
                temp.X = ((PictureBox)sender).Location.X + (e.X - MouseDownX);
                temp.Y = ((PictureBox)sender).Location.Y + (e.Y - MouseDownY);
                ((PictureBox)sender).Location = temp;
            }
            return;
        }

    }
}

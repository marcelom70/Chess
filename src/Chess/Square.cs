using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    /// <summary>
    /// This is a class that compose a board. Each one has its fixed, immutable position,
    /// and refers or not to a single volatile piece in the game. When it does not refers
    /// to a piece, it means it´s empty.
    /// </summary>
    public class Square
    {
        public enum Color { Light, Dark };

        // represents the horizontal position of the square in the board
        private int x;
        // represents the vertical position of the square in the board
        private int y;

        /// <summary>
        /// This is not a relevant information, it can only be useful for the Bishop
        /// </summary>
        private Color _color = Color.Light;

        Piece piece = null;

        public Square(int x, int y)
        {
            X = x;
            Y = y;

            // Sets the color of the current square
            if (((x % 2 == 0) & (y % 2 == 0)) | ((x % 2 != 0) & (y % 2 != 0)))
            {
                color = Color.Dark;
            }
        }

        /// <summary>
        /// This is not a relevant information, it can only be useful for the Bishop
        /// </summary>
        public Color color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        /// <summary>
        /// A piece placed in this instance of Square
        /// </summary>
        public Piece Piece
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;
            }
        }

        /// <summary>
        /// Represents a reference to the alphabetic sequence in the chess board
        /// </summary>
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        /// <summary>
        /// Represents a reference to the numeric sequence in the chess board
        /// </summary>
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public bool HasPiece { get { return this.Piece != null; } }

        public bool IsSameSquareColor(Piece.EColor color)
        {
            return this.Piece.Color == color;
        }


        /// <summary>
        /// Overrides the method in order to return a 
        /// chess notation known internationally for the square
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Board.STR_Y.Substring(Y - 1, 1) + X.ToString();
        }

    }
}

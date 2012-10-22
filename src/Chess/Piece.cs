using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    /// <summary>
    /// This is an abstract class that represents a generic piece. It can be inherited to implement
    /// specific behaviors of that kind of piece. It cannot be created. It refers to a square 
    /// where it sits. When it does not refers to a square, that means it´s out of the board.
    /// </summary>
    public abstract class Piece
    {
        private Square _square;

        public enum EColor { White, Black };

        //private Color _color;

        private bool _moved = false;

        //private string _prefix;

        //int _value;

        public Piece()
        { }

        /// <summary>
        /// Constructor able to receive the color
        /// </summary>
        /// <param name="color"></param>
        public Piece(EColor color)
        {
            this.Color = color;
        }

        /// <summary>
        /// Movement constraints are checked out in the pieces, since there are
        /// specific things to consider, for example, how many squares this piece
        /// is supposed to move. Every class related to a piece of the chess must
        /// implement this method and should call this one.
        /// </summary>
        /// <param name="targetSquare"></param>
        /// <returns></returns>
        public bool Movement(Square targetSquare)
        {
            // Basic validations...
            return (!targetSquare.HasPiece || !targetSquare.IsSameSquareColor(this.Color)) && SpecificMovement(targetSquare);
        }

        public abstract bool SpecificMovement(Square targetSquare);


        public EColor Color { get; set; }

        public bool Moved
        {
            get
            {
                return _moved;
            }
        }

        public string Prefix { get; set; }

        public Square square
        {
            get
            {
                return _square;
            }
            set
            {
                if (_square != null)
                {
                    _moved = true;
                }
                _square = value;
            }
        }

        public override string ToString()
        {
            return Prefix.ToString();
        }

        public int MaterialValue        {            get;            set ;        }

    }
}

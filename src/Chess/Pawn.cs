using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Chess
{
    /// <summary>
    /// This is the most ordinary piece in the game.
    /// </summary>
    public class Pawn : Piece
    {

        private int _progress = 1;

        public Pawn(EColor color, int number)
            : base(color)
        {
            if (color == EColor.Black)
            {
                _progress *= -1;
            }
            this.Prefix = "P" + number.ToString();
            this.MaterialValue = 0;
        }

    
        public override bool SpecificMovement(Square targetSquare)
        {
            if (targetSquare.Piece == null)
            {
                return ((targetSquare.Y == this.square.Y & targetSquare.X == this.square.X + (1 * _progress)) ||
                    (!this.Moved && (targetSquare.Y == this.square.Y & targetSquare.X
                                    == this.square.X + (2 * _progress))));
            }
            else
            {
                if (targetSquare.Piece.Color != this.Color)
                {
                    return (
                        Board.IsNextColumn (targetSquare, this.square )
                        //(targetSquare.Y - this.square.Y == 1 | this.square.Y - targetSquare.Y == 1)
                        & targetSquare.X == this.square.X + (1 * _progress));
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

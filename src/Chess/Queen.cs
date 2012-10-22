using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    /// <summary>
    /// This is the second most important piece in the game.
    /// </summary>
    public class Queen : Piece
    {
        public Queen(EColor color)
            : base(color)
        {
            this.Prefix = "Q";
            this.MaterialValue = 4;
        }


        public override bool SpecificMovement(Square targetSquare)
        {

            var movedOnDiagonal = Board.GetColumnDistance(targetSquare, this.square) == Board.GetRowDistance(this.square, targetSquare);
            var movedOnColumn = Board.IsSameColumn(targetSquare, this.square);
            var movedOnRow = Board.IsSameRow(this.square, targetSquare);

            return movedOnDiagonal || movedOnColumn || movedOnRow;
        }
    }

}



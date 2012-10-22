using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    /// <summary>
    /// This is the most important piece in the game.
    /// This kind of piece participates in a special movement called castling
    /// </summary>
    public class King : Piece
    {
        public King(EColor color)
            : base(color)
        {
            this.Prefix = "K";
            this.MaterialValue = 5;
        }


        public override bool SpecificMovement(Square targetSquare)
        {
            var movedOnDiagonal = Board.IsNextColumn(targetSquare, this.square) && Board.IsNextRow(targetSquare, this.square);
            var movedOnRow = Board.IsNextColumn(targetSquare, this.square) && Board.IsSameRow(targetSquare, this.square);
            var movedOnColumn = Board.IsNextRow(targetSquare, this.square) && Board.IsSameColumn(targetSquare, this.square); 

            return movedOnDiagonal || movedOnColumn || movedOnRow;

        }
    }
}

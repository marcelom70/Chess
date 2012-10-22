using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    /// <summary>
    /// This is the only piece in the game that is able to jump over the others
    /// </summary>
    public class Knight : Piece
    {
        public Knight(EColor color, int number)
            : base(color)
        {
            this.Prefix = "N" + number.ToString();
            this.MaterialValue = 1;
        }



        public override bool SpecificMovement(Square targetSquare)
        {
            var movedByColumn = (Board.IsNextColumn(targetSquare, this.square) && Board.IsNextColumn(targetSquare, this.square, 2)) |
                                (Board.IsNextColumn(targetSquare, this.square, 2) && Board.IsNextColumn(targetSquare, this.square));
            var movedByRow = (Board.IsNextColumn(targetSquare, this.square, 2) && Board.IsNextRow(targetSquare, this.square)) |
                             (Board.IsNextColumn(targetSquare, this.square) && Board.IsNextRow(targetSquare, this.square, 2));

            return movedByColumn || movedByRow;

        }

    }
}

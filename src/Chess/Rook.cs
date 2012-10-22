using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    /// <summary>
    /// This is the third most important piece in the game.
    /// This kind of piece participates in a special movement called castling
    /// </summary>
    public class Rook: Piece
    {
        public Rook(EColor color, int number)
            : base(color)
        {
            this.Prefix = "R" + number.ToString();
            this.MaterialValue = 3;
        }


        public override bool SpecificMovement(Square targetSquare)
        {
            var movedOnColumn = Board.IsSameColumn(targetSquare, this.square);
            var movedOnRow = Board.IsSameRow(this.square, targetSquare);

            return movedOnColumn || movedOnRow;
        }
        
    }
}

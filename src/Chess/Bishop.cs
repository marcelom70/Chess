using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    /// <summary>
    /// This is a kind of piece that can´t sit on a square of different color from its original one.
    /// </summary>
    public class Bishop : Piece
    {
        public Bishop(EColor color, int number)
            : base(color)
        {
            this.Prefix = "B" + number.ToString();
            this.MaterialValue = 2;
        }

  
        public override bool SpecificMovement(Square targetSquare){

            return Board.GetColumnDistance(targetSquare,this.square) == 
                Board.GetRowDistance(this.square, targetSquare);

        }
    }
}

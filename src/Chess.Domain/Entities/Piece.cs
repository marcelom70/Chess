using System;

namespace Chess.Domain.Entities
{
    public abstract class Piece
    {
        public string Color { get; protected set; }

        public Position Position { get; protected set; }

        public void AssignPosition(string position)
        {
            Position.SetPosition(position); 
        }

        public virtual bool AcceptDestiny(string destination)
        {
            //if(destination == Position)
            //    return false;
            //else
            //    return true;

            //HACK: better than older code, han?
            return destination != Position.ToString();
        }

        public override bool Equals(object obj)
        {
            var piece = obj as Piece;
            if (piece == null)
                return false;

            return piece.Position == this.Position && piece.Color == this.Color && piece.GetType() == this.GetType();
        }

        internal static int GetColumnNumber(char column)
        {
            return "ABCDEFGH".IndexOf(Char.ToUpper(column)) + 1;
        }

        internal static int GetDistance(int origin, int destiny)
        {
            return Math.Abs(origin - destiny);
        }

    }
}
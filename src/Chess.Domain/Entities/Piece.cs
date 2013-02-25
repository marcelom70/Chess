using System;

namespace Chess.Domain.Entities
{
    public abstract class Piece
    {
        public string Color { get; protected set; }

        public string Position { get; protected set; }

        public void AssignPosition(string position)
        {
            this.Position = position;
        }

        public bool AcceptDestiny(string destination)
        {
            //if(destination == Position)
            //    return false;
            //else
            //    return true;

            //HACK: better than older code, han?
            return destination != Position;
        }

        public override bool Equals(object obj)
        {
            var piece = obj as Piece;
            if (piece == null)
                return false;

            return piece.Position == this.Position && piece.Color == this.Color && piece.GetType() == this.GetType();
        }

        internal static int GetDistance(int origin, int destiny)
        {
            return Math.Abs(origin - destiny);
        }

    }
}
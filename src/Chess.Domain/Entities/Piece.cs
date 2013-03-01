using System;

namespace Chess.Domain.Entities
{
    public abstract class Piece
    {
        public string Color { get; protected set; }

        public string Position { get; protected set; }

        public void AssignPosition(string position)
        {
            Position = position;
        }

        public int GetRow(string position)
        {
            var charArray = position.ToCharArray();
            return int.Parse(charArray[1].ToString());
        }

        public int GetColumn(string position)
        {
            var charArray = position.ToCharArray();
            return "ABCDEFGH".IndexOf(Char.ToUpper(charArray[0])) + 1;
        }

        public virtual bool AcceptDestiny(string destination)
        {
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
namespace Chess.Domain.Entities
{
    public abstract class Piece
    {
        public string Colour { get; protected set; }

        public string Position { get; protected set; }

        public void AssignPosition(string position)
        {
            this.Position = position;
        }

        public bool AcceptDestiny(string destination)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            var piece = obj as Piece;
            if (piece == null)
                return false;

            return piece.Position == this.Position && piece.Colour == this.Colour && piece.GetType() == this.GetType();
        }
    }
}
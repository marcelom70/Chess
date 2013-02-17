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
    }
}
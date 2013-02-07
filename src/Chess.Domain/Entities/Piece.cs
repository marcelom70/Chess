namespace Chess.Domain.Entities
{
    public abstract class Piece
    {
        public string Colour { get; protected set; }

        public Square Square { get; private set; }

        public void AssignSquare(Square square)
        {
            this.Square = square;
        }
    }
}
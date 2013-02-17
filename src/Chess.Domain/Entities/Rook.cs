namespace Chess.Domain.Entities
{
    public class Rook:Piece
    {
        public Rook(string colour, string position)
        {
            Colour = colour;
            Position = position;
        }
    }
}
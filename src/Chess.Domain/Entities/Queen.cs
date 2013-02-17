namespace Chess.Domain.Entities
{
    public class Queen : Piece
    {
        public Queen(string colour, string position)
        {
            Colour = colour;
            Position = position;
        }
    }
}
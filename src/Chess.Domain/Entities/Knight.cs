namespace Chess.Domain.Entities
{
    public class Knight : Piece
    {
        public Knight(string colour, string position)
        {
            Colour = colour;
            Position = position;
        }
    }
}
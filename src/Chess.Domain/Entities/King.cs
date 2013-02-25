namespace Chess.Domain.Entities
{
    public class King : Piece
    {
        public King(string colour, string position)
        {
            Color = colour;
            Position = position;
        }
    }
}
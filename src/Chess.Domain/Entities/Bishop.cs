namespace Chess.Domain.Entities
{
    public class Bishop: Piece
    {
        public Bishop(string colour, string position)
        {
            Colour = colour;
            Position = position;
        }
    }
}
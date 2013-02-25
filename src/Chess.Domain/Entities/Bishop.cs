namespace Chess.Domain.Entities
{
    public class Bishop: Piece
    {
        public Bishop(string colour, string position)
        {
            Color = colour;
            Position = position;
        }
    }
}
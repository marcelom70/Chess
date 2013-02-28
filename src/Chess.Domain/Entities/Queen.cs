namespace Chess.Domain.Entities
{
    public class Queen : Piece
    {
        public Queen(string colour, string position)
        {
            Color = colour;
            AssignPosition(position);
        }
    }
}
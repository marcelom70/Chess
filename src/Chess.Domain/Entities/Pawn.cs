namespace Chess.Domain.Entities
{
    public class Pawn:Piece
    {
        public Pawn(string colour, string position)
        {
            Color = colour;
            AssignPosition(position);
        }
    }
}

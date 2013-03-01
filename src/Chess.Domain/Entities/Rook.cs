namespace Chess.Domain.Entities
{
    public class Rook:Piece
    {
        public Rook(string colour, string position)
        {
            Color = colour;
            AssignPosition(position);
        }

        public override bool AcceptDestiny(string destination)
        {
            if (!base.AcceptDestiny(destination))
                return false;
            else
            {
                return GetDistance(GetRow(Position), GetRow(destination)) == 0 |
                       GetDistance(GetColumn(Position), GetColumn(destination)) == 0;
            }
        }

    }
}
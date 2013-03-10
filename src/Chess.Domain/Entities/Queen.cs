namespace Chess.Domain.Entities
{
    public class Queen : Piece
    {
        public Queen(string colour, string position)
        {
            Color = colour;
            AssignPosition(position);
        }

        public override bool AcceptDestiny(string destination)
        {
            if (!base.AcceptDestiny(destination))
                return false;
            
            return (GetDistance(GetRow(Position), GetRow(destination)) ==
                    GetDistance(GetColumn(Position), GetColumn(destination))) |
                   (GetDistance(GetRow(Position), GetRow(destination)) == 0 |
                    GetDistance(GetColumn(Position), GetColumn(destination)) == 0);
        }
    }
}
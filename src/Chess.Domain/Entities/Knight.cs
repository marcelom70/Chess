namespace Chess.Domain.Entities
{
    public class Knight : Piece
    {
        public Knight(string colour, string position)
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
                return (GetDistance(GetRow(Position), GetRow(destination)) == 2 &
                       GetDistance(GetColumn(Position), GetColumn(destination)) == 1) |
                       (GetDistance(GetRow(Position), GetRow(destination)) == 1 &
                       GetDistance(GetColumn(Position), GetColumn(destination)) == 2);

            }
        }

    }
}
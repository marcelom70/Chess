namespace Chess.Domain.Entities
{
    public class Bishop: Piece
    {
        public Bishop(string colour, string position)
        {
            Color = colour;
            AssignPosition(position);
        }

        public override bool AcceptDestiny(string destination)
        {
            if (!base.AcceptDestiny(destination))
                return false;
            
            return GetDistance(GetRow(Position), GetRow(destination)) ==
                   GetDistance(GetColumn(Position), GetColumn(destination));
        }
    }
}
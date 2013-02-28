namespace Chess.Domain.Entities
{
    public class King : Piece
    {
        public King(string colour, string position)
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
               
            }
            
        }

    }
}
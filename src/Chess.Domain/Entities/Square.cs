namespace Chess.Domain.Entities
{
    public class Square
    {
        public Square (int row  , char column)
        {
            this.Column = column;
            this.Row = row;
        }

        public int Row { get; private set; }
        public char Column { get; private set; }
    }
}
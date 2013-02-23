namespace Chess.Domain.Entities
{
    public class Path
    {
        public string Origin { get; private set; }
        public string Destiny { get;private  set; }

        public Path()
        {
        }

        public Path(string command)
        {
            command = command.ToUpperInvariant();

            Origin = command.Substring(0, 2);
            Destiny = command.Substring(2, 2);
        }

        public override string ToString()
        {
            return Origin + Destiny;
        }
    }
}
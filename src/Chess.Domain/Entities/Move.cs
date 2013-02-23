
namespace Chess.Domain.Entities
{
    public class Move
    {
        public string Origin { get; set; }
        public string Destiny { get; set; }
        public string Result { get; set; }

        public Move(Path path)
        {
            Origin= path.Origin;
            Destiny= path.Destiny;
        }
    }
}
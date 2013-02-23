
namespace Chess.Domain.Entities
{
    public class Move
    {
        //public string Origin { get; set; }
        //public string Destiny { get; set; }
        //public string Result { get; set; }

        public string Command { get; private set; }

        public Move(string command)
        {
            Command = command;
        }
    }
}
<<<<<<< HEAD
namespace Chess.Domain.Entities
{
    public class Move
    {
        public string Origin { get; set; }
        public string Destiny { get; set; }
        public string Result { get; set; }
    }
=======
namespace Chess.Domain.Entities
{
    public class Move
    {
        public string Command { get; private set; }

        public Move(string command)
        {
            Command = command;
        }

    }
>>>>>>> 955d105fa2e2d79810dcbc94abce1b8b6a113f49
}
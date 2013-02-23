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
}
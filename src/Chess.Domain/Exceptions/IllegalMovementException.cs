using EasyArchitecture.Instances.Validation;

namespace Chess.Domain.Exceptions
{
    public class IllegalMovementException:DomainException
    {
        public IllegalMovementException(string message):base(message)
        {
        }
    }
}

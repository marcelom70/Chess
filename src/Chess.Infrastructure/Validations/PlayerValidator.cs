using System;
using Chess.Domain.Entities;
using EasyArchitecture.Plugins.BultIn.Validation;

namespace Chess.Infrastructure.Validations
{
    public class PlayerValidator : ValidationRuleSet<Player>
    {
        public PlayerValidator()
        {
            AddRule(x => x.Id == Guid.Empty, "Player should have identification");
            AddRule(x => string.IsNullOrEmpty(x.Name), "Player must have a name");
        }
    }
}

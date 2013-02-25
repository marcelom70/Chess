using System;
using Chess.Domain;
using Chess.Domain.Entities;
using EasyArchitecture.Validation.Plugin.BultIn;

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

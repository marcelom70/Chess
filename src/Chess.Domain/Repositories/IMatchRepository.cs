using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain.Repositories
{
    public interface IMatchRepository
    {
        Guid Save(Match match);
        Match Get(Guid matchId);
    }
}

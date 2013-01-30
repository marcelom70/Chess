using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Domain.Entities;
using EasyArchitecture.Persistence;

namespace Chess.Domain.Repositories
{
    public interface IMatchRepository:IRepository<Match>
    {
    }
}

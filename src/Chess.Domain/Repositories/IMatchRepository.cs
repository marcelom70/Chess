using System;
using Chess.Domain.Entities;
using EasyArchitecture.Persistence;

namespace Chess.Domain.Repositories
{
    public interface IMatchRepository:IRepository<Match>
    {
        Match Get(Guid id);
    }
}

using System;
using Chess.Domain.Entities;
using EasyArchitecture.Mechanisms.Persistence;

namespace Chess.Domain.Repositories
{
    public interface IMatchRepository:IRepository<Match>
    {
        Match Get(Guid id);
    }
}

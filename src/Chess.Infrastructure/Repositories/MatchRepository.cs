using System;
using System.Linq;
using Chess.Domain;
using Chess.Domain.Entities;
using Chess.Domain.Repositories;
using EasyArchitecture.Persistence;

namespace Chess.Infrastructure.Repositories
{
    public class MatchRepository:Repository<Match>,IMatchRepository
    {
        public new void Save(Match entity)
        {
            entity.Id = Guid.NewGuid();
            base.Save(entity);
        }

        public Match Get(Guid id)
        {
            var qbe = new Match() { Id = id };
            return Get(qbe).FirstOrDefault();
        }
    }
}

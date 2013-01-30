using System;
using System.Collections.Generic;

namespace Chess.Domain.Entities
{
    public class Match
    {
        public Board Board {get;set;}
        public Player Player{ get; set; } 
        public IList<Move> Moves { get; set; }

        public Guid Id { get; set; }

        public void InitializeBoard()
        {
            throw new System.NotImplementedException();
        }

        public void DefinePlayer()
        {
            throw new System.NotImplementedException();
        }

        public void Move(string command)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;

namespace Chess.Domain.Entities
{
    public class Match
    {
        public Board Board {get;set;}
        public Player WhitePlayer{ get; set; }
        public Player BlackPlayer { get; set; }
        public IList<Move> Moves { get; set; }

        public Match()
        { }

        public Match(Player whitePlayer, Player blackPlayer)
        {
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
        }

        public Guid Id { get; set; }

        public void InitializeBoard()
        {
            Board = new Board();
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
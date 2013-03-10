using System;
using System.Collections.Generic;
using Chess.Domain.Exceptions;

namespace Chess.Domain.Entities
{
    public class Match
    {
        private Board _board;
        public Player WhitePlayer{ get; private set; }
        public Player BlackPlayer { get; private set; }
        public IList<Move> Moves { get; set; }
        public Guid Id { get; set; }

        public Match()
        { }

        public Match(Player whitePlayer, Player blackPlayer)
        {
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
        }

        public void Initialize(string boardConfiguration)
        {
            _board = new Board(boardConfiguration);
            Moves = new List<Move>();
        }

        public string Move(Path path)
        {
            _board.AcceptPosition(path.Origin);
            _board.AcceptPosition(path.Destiny);
            
            var piece = _board.GetPiece(path.Origin);
            if (piece == null)
                throw new IllegalMovementException("There's no piece at this position");
            
            if (Moves.Count % 2 == (piece.Color == "black" ? 0 : 1))
               throw new IllegalMovementException("It´s not supposed to be this player turn");

            if (!piece.AcceptDestiny(path.Destiny))
                throw new IllegalMovementException("Can´t move to this position");

            //verificar se ha outra peca no caminho do movimento (knight exception)
            

            Moves.Add(new Move(path));

            return path.ToString();
        }
    }
}
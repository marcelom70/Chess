using System;
using System.Collections.Generic;

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
            //Moves = new List<Move>();
        }

        public string Move(string command)
        {
            command = command.ToUpperInvariant();

            //validar command

            //obter posicoes
            var orign = command.Substring(0, 2);
            var destination = command.Substring(2, 2);

            //validar limites do board
            _board.AcceptPosition(orign);
            _board.AcceptPosition(destination);
            
            //localizar a peca
            var piece = _board.GetPiece(orign);

            //se nao houver peca?
            if (piece == null)
                throw new Exception();
            
            if(Moves.Count % 2 == 0)
               throw new Exception("It´s not supposed to be this player turn");

            //verificar se a peca aceita seu destino hehe
            if (!piece.AcceptDestiny((string)destination))
                throw new Exception("Can´t move to the same position");

            //verificar se ha outra peca no caminho do movimento (knight exception)
            return command;
            //...

            //adicionar à lista de movimentos
            //throw new System.NotImplementedException();
        }
    }
}
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

            //verificar se a peca aceita seu destino hehe
            if (!piece.AcceptDestiny((string)destination))
                throw new Exception("Can´t move to the same position");

            //adicionar à lista de movimentos (se tudo deu certo)
            //por enquanto coloquei o resultado como command
            this.Moves.Add(new Move(){Origin = orign,Destiny = destination,Result = command});


            //verificar se ha outra peca no caminho do movimento (knight exception)
            return command;
            //...

            
            //throw new System.NotImplementedException();
        }
    }
}
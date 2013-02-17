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

        public void Initialize()
        {
            _board = new Board();
        }

        public void Move(string command)
        {
            command = command.ToUpperInvariant();

            //validar command

            //obter posicoes
            var orign = command.Substring(0, 2);
            var destination = command.Substring(2, 2);

            //validar limites do board


            //localizar a peca
            var piece = _board.GetPiece(orign);

            //se nao houver peca?

            //verificar se a peca aceita seu destino hehe
            //piece.AcceptDestiny(destination);

            //verificar se ha outra peca no caminho do movimento (knight exception)

            //...

            //adicionar à lista de movimentos
            //throw new System.NotImplementedException();
        }
    }
}
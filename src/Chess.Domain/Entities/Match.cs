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
            //validar e interpretar comando
            var @params = command.ToUpperInvariant().ToCharArray();

            //deveria ter validado =(

            //localizar squares
            var orign = _board.GetSquare(@params[0], int.Parse(@params[1].ToString()));
            var destination = _board.GetSquare(@params[2], int.Parse(@params[3].ToString()));

            //localizar a peca
            var piece = _board.GetPiece(orign);

            //verificar se a peca aceita seu destino hehe
            //piece.AcceptDestiny(destination);

            //verificar se ha outra peca no caminho do movimento (knight exception)

            //...

            //adicionar à lista de movimentos
            //throw new System.NotImplementedException();
        }
    }
}
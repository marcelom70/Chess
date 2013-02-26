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
            Moves = new List<Move>();
        }

        public string Move(Path path)
        {
            //validar limites do board
            _board.AcceptPosition(path.Origin);
            _board.AcceptPosition(path.Destiny);
            
            //localizar a peca
            var piece = _board.GetPiece(path.Origin);

            //se nao houver peca?
            if (piece == null)
                throw new Exception();
            
            if (Moves.Count % 2 == (piece.Color == "black" ? 0 : 1))
               throw new Exception("It´s not supposed to be this player turn");

            //verificar se a peca aceita seu destino hehe
            if (!piece.AcceptDestiny(path.Destiny))
                throw new Exception("Can´t move to the same position");

            //adicionar à lista de movimentos (se tudo deu certo)
            //por enquanto coloquei o resultado como command
            Moves.Add(new Move(path));


            //verificar se ha outra peca no caminho do movimento (knight exception)
            return path.ToString();
        }
    }
}
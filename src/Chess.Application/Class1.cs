using Chess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Application
{
    public class ChessFacade
    {
        public Guid SetUpMatch() {
            var match = new Match(); //cria um novo match
            
            //match.InitializeBoard();

            //match.DefinePlayer(); //definir nome do jogador/cor peças

            var matchId = Guid.NewGuid();// _repository.Save(match); 
            //outra alternativa eh recuperar identificador natural, como o nome dos jogadores do jogo
            //e sempre que for fazer um movimento, utiliza-los para recuperar o jogo 

            return matchId;
        }

        public void DoMove(string command, Guid matchId) { 
        
            //command like "e2e4" notacao completa ou abreviada

            //var match = _repository.Get(matchId);
            //match.Move(command);

            //retornar de alguma forma - ilegal move, move ok, etc
        }
    }
}

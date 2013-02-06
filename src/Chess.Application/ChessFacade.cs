using Chess.Application.Contracts;
using Chess.Application.Contracts.DTOs;
using Chess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Domain.Entities;
using Chess.Domain.Repositories;
using EasyArchitecture.Translation;

namespace Chess.Application
{
    public class ChessFacade : IChessFacade
    {
        private readonly IMatchRepository _repository;
        private Chess.Domain.Player _whitePlayer;
        private Chess.Domain.Player _blackPlayer;

        public ChessFacade(IMatchRepository repository)
        {
            _repository = repository;
        }

        public Guid SetUpMatch(PlayerDTO whitePlayer, PlayerDTO blackPlayer) {
            
            //comentarios sao apenas linhas de pensamento

            //converter DTOs para entidades
            _whitePlayer = Translator.This(whitePlayer).To<Chess.Domain.Player>();
            _blackPlayer = Translator.This(blackPlayer).To<Chess.Domain.Player>();

            //validar as entidades?

            //novo match
            var match = new Match(_whitePlayer, _blackPlayer); 

            //aqui ele deve saber setar a posicao inicial das pecas
            match.InitializeBoard();

            //passar os players? ou devia ser feito no constructor do match?
            //match.DefinePlayer(); 


            _repository.Save(match); 


            return match.Id;
        }

        public void DoMove(string command, Guid matchId) { 
        
            //command like "e2e4" notacao completa ou abreviada

            var qbe = new Match() {Id = matchId};
            var match = _repository.Get(qbe).FirstOrDefault();
            if (match != null) match.Move(command);

            //retornar de alguma forma - ilegal move, move ok, etc
        }
    }
}

using Chess.Application.Contracts;
using Chess.Application.Contracts.DTOs;
using Chess.Domain;
using System;
using System.Linq;
using Chess.Domain.Entities;
using Chess.Domain.Repositories;
using EasyArchitecture.Translation;
using EasyArchitecture.Validation;

namespace Chess.Application
{
    public class ChessFacade : IChessFacade
    {
        private readonly IMatchRepository _repository;

        public ChessFacade(IMatchRepository repository)
        {
            _repository = repository;
        }

        public Guid SetUpMatch(PlayerDTO whitePlayerDTO, PlayerDTO blackPlayerDTO)
        {
            var whitePlayer = Translator.This(whitePlayerDTO).To<Player>();
            var blackPlayer = Translator.This(blackPlayerDTO).To<Player>();

            Validator.This(whitePlayer).IsValid();
            Validator.This(blackPlayer).IsValid();

            var match = new Match(whitePlayer, blackPlayer); 

            //aqui ele deve saber setar a posicao inicial das pecas
            match.InitializeBoard();

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

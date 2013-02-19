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
            return CreateMatch(whitePlayerDTO, blackPlayerDTO,null);
        }

        private Guid CreateMatch(PlayerDTO whitePlayerDTO, PlayerDTO blackPlayerDTO, string boardConfiguration)
        {
            var whitePlayer = Translator.This(whitePlayerDTO).To<Player>();
            var blackPlayer = Translator.This(blackPlayerDTO).To<Player>();

            Validator.This(whitePlayer).IsValid();
            Validator.This(blackPlayer).IsValid();

            var match = new Match(whitePlayer, blackPlayer);

            match.Initialize(boardConfiguration);

            _repository.Save(match);

            return match.Id;
        }

        public Guid SetUpMatch(PlayerDTO whitePlayerDTO, PlayerDTO blackPlayerDTO, string boardConfiguration)
        {
            return CreateMatch(whitePlayerDTO, blackPlayerDTO, boardConfiguration);
        }

        public string DoMove(string command, Guid matchId) { 
        
            //command like "e2e4" notacao completa ou abreviada

            var qbe = new Match() {Id = matchId};
            var match = _repository.Get(qbe).FirstOrDefault();
            if (match == null) 
                return null; 
            
            return match.Move(command);

            //retornar de alguma forma - ilegal move, move ok, etc
        }
    }
}

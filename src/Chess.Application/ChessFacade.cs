using Chess.Application.Contracts;
using Chess.Application.Contracts.DTOs;
using System;
using Chess.Domain.Entities;
using Chess.Domain.Repositories;
using EasyArchitecture.Mechanisms.Translation;
using EasyArchitecture.Mechanisms.Validation;

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

        public string DoMove(string command, Guid matchId)
        {
            //TODO: aumentar a quandiade de precondicoes, utilizar design by contract
            if (string.IsNullOrEmpty(command))
                throw new ArgumentNullException("command");

            var path = Translator.This(command).To<Path>();

            Validator.This(path).IsValid();

            var match = _repository.Get(matchId);
            return match == null ? null : match.Move(path);
        }
    }
}

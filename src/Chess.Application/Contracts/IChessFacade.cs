using System;
using Chess.Application.Contracts.DTOs;

namespace Chess.Application.Contracts
{
    public interface IChessFacade
    {
        Guid SetUpMatch(PlayerDTO whitePlayer, PlayerDTO blackPlayer);
        void DoMove(string command, Guid matchId);
    }
}
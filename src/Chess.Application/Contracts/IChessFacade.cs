using System;
using Chess.Application.Contracts.DTOs;

namespace Chess.Application.Contracts
{
    public interface IChessFacade
    {
        Guid SetUpMatch(PlayerDTO whitePlayer, PlayerDTO blackPlayer);
        //boardconfiguration: http://pt.wikipedia.org/wiki/Nota%C3%A7%C3%A3o_Forsyth
        Guid SetUpMatch(PlayerDTO whitePlayer, PlayerDTO blackPlayer, string boardConfiguration);
        string DoMove(string command, Guid matchId);
    }
}
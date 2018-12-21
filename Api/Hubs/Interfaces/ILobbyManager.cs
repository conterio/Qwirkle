using Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Api.Hubs.Interfaces
{
    public interface ILobbyManager
    {
        bool JoinGame(Guid gameId, IPlayer player);
        void StartGame();
        bool AddPlayer();
        List<IPlayer> GetAvailablePlayers();
    }
}
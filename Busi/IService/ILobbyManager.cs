using Models;
using System;
using System.Collections.Generic;

namespace Busi.IService
{
    public interface ILobbyManager
    {
        bool JoinGame(Guid gameId, Player player);
        void StartGame(Guid gameId);
        bool SignalAddPlayer(Guid gameId, Player player);
        List<Player> GetAvailablePlayers();
    }
}
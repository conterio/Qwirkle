using Models;
using System;
using System.Collections.Generic;

namespace Busi.IService
{
    public interface ILobbyManager
    {
        bool JoinGame(Guid gameId, string playerConnectionId);
        void StartGame(Guid gameId);
        bool SignalAddPlayer(Guid gameId, string playerConnectionId);
        List<Player> GetAvailablePlayers();
    }
}
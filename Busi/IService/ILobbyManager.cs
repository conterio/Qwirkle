using Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Busi.IService
{
    public interface ILobbyManager
    {
        bool JoinGame(Guid gameId, IPlayer player);
        void StartGame(Guid gameId);
        bool SignalAddPlayer(Guid gameId, IPlayer player);
        List<IPlayer> GetAvailablePlayers();
    }
}
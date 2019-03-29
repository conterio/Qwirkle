using Models;
using Models.ClientOutBound;
using System;
using System.Collections.Generic;

namespace Busi.IService
{
    public interface ILobbyManager
    {
        void JoinGame(Guid gameId);
		void LeaveGame(Guid gameId);
        void StartGame(Guid gameId);
        bool SignalAddPlayer(Guid gameId, string playerConnectionId);
        List<PlayerViewModel> GetAvailablePlayers();
    }
}
using Models;
using Models.ClientCommands;
using Models.ClientOutBound;
using System;
using System.Collections.Generic;

namespace Busi.IBusi
{
    public interface IGameBusi
    {
        AvailableGameViewModel CreateGame(GameSettings settings, string hostId);
        List<AvailableGameViewModel> GetLobbies();
        void StartGame(Guid gameId);
	    void AddPlayer(Guid gameId, string playerConnectionId);
        void PlayTiles(string playerConnectionId, PlayTilesTurn turn);
        void SwapTiles(string playerConnectionId, SwapTilesTurn turn);
		void RemovePlayer(Guid gameId, string playerId);
    }
}
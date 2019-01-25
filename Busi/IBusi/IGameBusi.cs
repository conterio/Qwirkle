using Models;
using Models.ViewModels;
using System;

namespace Busi.IBusi
{
    public interface IGameBusi
	{
		void StartGame(Guid gameId);
	    void AddPlayer(Guid gameId, Player player);
        void PlayTiles(string playerConnectionId, PlayTilesTurnViewModel turn);
        void SwapTiles(string playerConnectionId, SwapTilesTurnViewModel turn);
    }
}
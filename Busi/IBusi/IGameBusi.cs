using Models;
using Models.ClientCommands;
using System;

namespace Busi.IBusi
{
    public interface IGameBusi
	{
		void StartGame(Guid gameId);
	    void AddPlayer(Guid gameId, Player player);
        void PlayTiles(string playerConnectionId, PlayTilesTurn turn);
        void SwapTiles(string playerConnectionId, SwapTilesTurn turn);
    }
}
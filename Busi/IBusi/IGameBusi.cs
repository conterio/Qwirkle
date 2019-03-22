using Models;
using Models.ClientCommands;
using System;
using System.Collections.Generic;

namespace Busi.IBusi
{
    public interface IGameBusi
    {
        Game CreateGame(GameSettings settings);
        List<Game> GetLobbies();
        void StartGame(Guid gameId);
	    void AddPlayer(Guid gameId, string playerConnectionId);
        void PlayTiles(string playerConnectionId, PlayTilesTurn turn);
        void SwapTiles(string playerConnectionId, SwapTilesTurn turn);
    }
}
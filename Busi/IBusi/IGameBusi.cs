using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Busi.IBusi
{
	public interface IGameBusi
	{
		Guid CreateGame();
		bool JoinGame(Guid gameId);
		bool JoinGameAsObserver(Guid gameid);
		Dictionary<Player, List<Tile>> StartGame(Guid gameId);
		PlayTurnResponse PlayMove(Guid GameId, ITurn move, Player player);
	}
}

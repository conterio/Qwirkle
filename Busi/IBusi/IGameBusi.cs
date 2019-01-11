using Models;
using Models.Interfaces;
using System;

namespace Busi.IBusi
{
    public interface IGameBusi
	{
		Game StartGame(Guid gameId);
	    Game AddPlayer(Guid gameId, IPlayer player);
	}
}
using Models.Interfaces;
using System;

namespace Busi.IBusi
{
    public interface IGameBusi
	{
		void StartGame(Guid gameId);
	    void AddPlayer(Guid gameId, IPlayer player);
	}
}
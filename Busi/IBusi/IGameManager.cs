using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Busi.IBusi
{
	public interface IGameManager
	{
		Guid CreateGame();
		bool IsMoveValid(ITurn move);
		List<List<Tile>> ApplyMove(ITurn move);
	}
}

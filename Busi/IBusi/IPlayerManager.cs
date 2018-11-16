using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Busi.IBusi
{
	public interface IPlayerManager
	{
		bool JoinGame(Guid playerId);
		PlayTurnResponse PlayMove(Guid playerId, ITurn move);
		bool Disqualify(Guid playerId);
	}
}

using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Busi.IBusi
{
	public interface IPlayerBusi
	{
		void InvalidatePlayer(string connectionId);
		bool RemoveTilesFromHand(List<Tile> tiles, string playerConnectionId);
        void AddTilesToHand(List<Tile> tiles, string playerConnectionId);
	}
}
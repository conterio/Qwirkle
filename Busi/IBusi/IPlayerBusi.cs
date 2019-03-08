using Models;
using System.Collections.Generic;

namespace Busi.IBusi
{
    public interface IPlayerBusi
	{
		void InvalidatePlayer(string connectionId);
		bool RemoveTilesFromHand(List<Tile> tiles, string playerConnectionId);
        void AddTilesToHand(List<Tile> tiles, string playerConnectionId);
		void AddScore(int value, string playerConnectionId);
	}
}
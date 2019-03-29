using Models;
using System.Collections.Generic;

namespace Busi.IBusi
{
    public interface IPlayerBusi
	{
		void InvalidatePlayer(string connectionId, string groupId);
		bool RemoveTilesFromHand(List<Tile> tiles, string playerConnectionId);
        void AddTilesToHand(List<Tile> tiles, string playerConnectionId);
		void AddScore(int value, string playerConnectionId);
        void AddPlayer(string connectionId, string playerName, bool isHumanPlayer);
        List<Player> GetAllPlayers();
        Player GetPlayer(string connectionId);
    }
}
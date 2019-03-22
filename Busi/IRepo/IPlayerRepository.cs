using Models;
using System.Collections.Generic;

namespace Busi.IRepo
{
    public interface IPlayerRepository
    {
        void AddPlayer(string connectionId, string playerName, bool isHumanPlayer);
        List<Player> GetAllPlayers();
        Player GetPlayer(string connectionId);
    }
}
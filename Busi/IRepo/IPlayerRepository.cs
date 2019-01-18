using Models.Interfaces;
using System.Collections.Generic;

namespace Busi.IRepo
{
    public interface IPlayerRepository
    {
        void AddPlayer(string connectionId, string playerName);
        List<IPlayer> GetAllPlayers();
    }
}
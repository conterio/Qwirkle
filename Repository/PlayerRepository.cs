using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using Models.Interfaces;
using System.Linq;

namespace Repository
{
    public interface IPlayerRepository 
    {
        void AddPlayer(string connectionId, IPlayer player);
        List<IPlayer> GetAllPlayers();
    }
    public class PlayerRepository : IPlayerRepository
    {
        private ConcurrentDictionary<string, IPlayer> Players { get; set; }

        public void AddPlayer(string connectionId, IPlayer player)
        {
            Players.AddOrUpdate(connectionId, player, (_,__) => player);
        }
        public List<IPlayer> GetAllPlayers()
        {
            return Players.Values.ToList();
        }
    }
}

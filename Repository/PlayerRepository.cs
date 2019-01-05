using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using Models.Interfaces;
using System.Linq;
using Models;

namespace Repository
{
    public interface IPlayerRepository
    {
        void AddPlayer(string connectionId, string playerName);
        List<IPlayer> GetAllPlayers();
    }
    public class PlayerRepository : IPlayerRepository
    {
        public PlayerRepository()
        {
            Players = new ConcurrentDictionary<string, IPlayer>();
        }

        private ConcurrentDictionary<string, IPlayer> Players { get; set; }

        public void AddPlayer(string connectionId, string playerName)
        {
            var player = new Player()
            {
                ConnectionId = connectionId,
                Name = playerName
            };
            Players.AddOrUpdate(connectionId, player, (_, __) => player);
        }
        public List<IPlayer> GetAllPlayers()
        {
            return Players.Values.ToList();
        }
    }
}
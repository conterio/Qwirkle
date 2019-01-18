using Models;
using Models.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Busi.IRepo;

namespace Repository
{
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
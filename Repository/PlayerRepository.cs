using Models;
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
            Players = new ConcurrentDictionary<string, Player>();
        }

        private ConcurrentDictionary<string, Player> Players { get; set; }

        public void AddPlayer(string connectionId, string playerName, bool isHumanPlayer)
        {
			var player = new Player()
			{
				IsHumanPlayer = isHumanPlayer,
                ConnectionId = connectionId,
                Name = playerName
            };
            Players.AddOrUpdate(connectionId, player, (_, __) => player);
        }
        public List<Player> GetAllPlayers()
        {
            return Players.Values.ToList();
        }

        public Player GetPlayer(string connectionId)
        {
            return Players.Values.SingleOrDefault(p => p.ConnectionId == connectionId);
        }
    }
}
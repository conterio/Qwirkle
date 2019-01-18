using Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Busi.IRepo;
using Models.Enums;

namespace Repository
{
    public class GameRepository : IGameRepository
    {
        private ConcurrentDictionary<Guid, Game> Games { get; set; }

        public GameRepository() {
            Games = new ConcurrentDictionary<Guid, Game>();
        }

        public Game GetGame(Guid guid)
        {
            Games.TryGetValue(guid, out var game);
            return game;
        }

        public Game CreateGame(GameSettings gameSettings)
        {
            gameSettings.GameId = Guid.NewGuid();
            var game = new Game() { GameSettings = gameSettings };
            Games.AddOrUpdate(gameSettings.GameId, game, (_, __) => game);
            return game;
        }

        public List<Game> GetLobbies()
        {
            return Games.Values.Where(g => g.Status == GameStatus.Lobby).ToList();
        }
    }
}
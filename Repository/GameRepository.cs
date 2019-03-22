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
            //TODO we could be creating games that never get cleaned up.
            //TODO we should add the host to the game
            //TODO add a way for players to leave the game and clean up game if there is no one left in the game
            var game = new Game {GameSettings = gameSettings, GameId = Guid.NewGuid()};
            Games.AddOrUpdate(game.GameId, game, (_, __) => game);
            return game;
        }

        public List<Game> GetLobbies()
        {
            return Games.Values.Where(g => g.Status == GameStatus.Lobby).ToList();
        }
    }
}
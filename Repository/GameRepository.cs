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
        private ConcurrentDictionary<Guid, Game> games { get; set; }

        public GameRepository() {
            games = new ConcurrentDictionary<Guid, Game>();
        }

        public Game GetGame(Guid gameId)
        {
            games.TryGetValue(gameId, out var game);
            return game;
        }

        public Game CreateGame(GameSettings gameSettings)
        {
            //TODO add a way for players to leave the game and clean up game if there is no one left in the game
            var game = new Game {GameSettings = gameSettings, GameId = Guid.NewGuid()};
            games.AddOrUpdate(game.GameId, game, (_, __) => game);
            return game;
        }

        public List<Game> GetLobbies()
        {
            return games.Values.Where(g => g.Status == GameStatus.Lobby &&
                                           g.GameSettings.MaxPlayers > g.Players.Where(p => !p.IsSpectator).Count()).ToList();
        }

        public void CleanUpGame(Guid gameId)
        {
			games.TryRemove(gameId, out var _);
        }
    }
}
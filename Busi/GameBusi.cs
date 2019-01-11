using Busi.Helpers;
using Busi.IBusi;
using Models.Enums;
using Repository;
using System;
using Models;
using Models.Interfaces;

namespace Busi
{
    public class GameBusi : IGameBusi
    {
        private readonly IGameRepository _gameRepository;
        private readonly IShuffleHelper _shuffleHelper;

        public GameBusi(IGameRepository gameRepository, IShuffleHelper shuffleHelper)
        {
            _gameRepository = gameRepository;
            _shuffleHelper = shuffleHelper;
        }

        public Game StartGame(Guid gameId)
        {
            var game = _gameRepository.GetGame(gameId);
            game.Status = GameStatus.InProgress;
            _shuffleHelper.Shuffle(game.Players);
            return game;
        }

        public Game AddPlayer(Guid gameId, IPlayer player)
        {
            var game = _gameRepository.GetGame(gameId);
            game.Players.Add(player);
            return game;
        }
    }
}
using Busi.Helpers;
using Busi.IBusi;
using Busi.IRepo;
using Busi.IService;
using Models.Enums;
using Models.Interfaces;
using System;

namespace Busi
{
    public class GameBusi : IGameBusi
    {
        private readonly IGameRepository _gameRepository;
        private readonly IShuffleHelper _shuffleHelper;
        private readonly IUpdater.IUpdater _updater;

        public GameBusi(IGameRepository gameRepository, IShuffleHelper shuffleHelper, IUpdater.IUpdater updater)
        {
            _gameRepository = gameRepository;
            _shuffleHelper = shuffleHelper;
            _updater = updater;
        }

        public void StartGame(Guid gameId)
        {
            var game = _gameRepository.GetGame(gameId);
            game.Status = GameStatus.InProgress;
            _shuffleHelper.Shuffle(game.Players);
            _updater.UpdateGroup(game.GameId.ToString(),nameof(IGameActions.GameStarted), game.GetViewModel());
            _updater.UpdateClient(game.Players[0].ConnectionId, nameof(IGameActions.SignalTurn), game.GetViewModel());
        }

        public void AddPlayer(Guid gameId, IPlayer player)
        {
            var game = _gameRepository.GetGame(gameId);
            game.Players.Add(player);
            _updater.UpdateGroup(game.GameId.ToString(), nameof(IGameActions.LobbyState), game.GetViewModel());
        }
    }
}
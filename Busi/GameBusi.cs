using Busi.Helpers;
using Busi.IBusi;
using Busi.IRepo;
using Busi.IService;
using Models;
using Models.Enums;
using Models.ViewModels;
using System;

namespace Busi
{
    public class GameBusi : IGameBusi
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IShuffleHelper _shuffleHelper;
        private readonly IUpdater.IUpdater _updater;

        public GameBusi(IGameRepository gameRepository, IPlayerRepository playerRepository, IShuffleHelper shuffleHelper, IUpdater.IUpdater updater)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _shuffleHelper = shuffleHelper;
            _updater = updater;
        }

        public void StartGame(Guid gameId)
        {
            var game = _gameRepository.GetGame(gameId);
            game.Status = GameStatus.InProgress;
            _shuffleHelper.Shuffle(game.Players);
            game.CurrentTurnPlayerId = game.Players[0].ConnectionId;

            foreach (var player in game.Players)
            {
                player.CurrentHand = game.TileBag.DrawTiles(game.GameSettings.HandSize);
                _updater.UpdateClient(player.ConnectionId, nameof(IGameActions.SignalGameState), game.GetViewModel(player.ConnectionId));
            }
        }

        public void AddPlayer(Guid gameId, Player player)
        {
            var game = _gameRepository.GetGame(gameId);
            game.Players.Add(player);
            _updater.UpdateGroup(game.GameId.ToString(), nameof(IGameActions.SignalGameState), game.GetViewModel(null));
        }

        public void PlayTiles(string playerConnectionId, PlayTilesTurnViewModel turn)
        {
            throw new NotImplementedException();
        }

        public void SwapTiles(string playerConnectionId, SwapTilesTurnViewModel turn)
        {
            var game = _gameRepository.GetGame(turn.GameId);
            if (game.CurrentTurnPlayerId != playerConnectionId)
            {
                //TODO invalidate that player. It's not their turn.
                return;
            }

            var player = _playerRepository.GetPlayer(playerConnectionId);

            //drawTiles if the player tries to draw more tiles than
            //there are in the bag That is an invalid move.
            if (game.TileBag.Count() < turn.TurnedInTiles.Count)
            {
                //TODO invalid turn. Tried to draw too many tiles
                return;
            }
            var newTiles = game.TileBag.DrawTiles(turn.TurnedInTiles.Count);
            foreach (var tile in turn.TurnedInTiles)
            {
                if (!player.CurrentHand.Contains(tile))
                {
                    //TODO invalid turn. They are trying to turn in a tile that wasn't in their hand.
                }

                player.CurrentHand.Remove(tile);
            }
            game.TileBag.ReturnTiles(turn.TurnedInTiles);
            player.CurrentHand.AddRange(newTiles);
        }
    }
}
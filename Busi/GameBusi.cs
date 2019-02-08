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
		private readonly IPlayerBusi _playerBusi;

        public GameBusi(IGameRepository gameRepository, IPlayerRepository playerRepository, IShuffleHelper shuffleHelper, IUpdater.IUpdater updater, IPlayerBusi playerBusi)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _shuffleHelper = shuffleHelper;
            _updater = updater;
            _playerBusi = playerBusi;
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
            var game = _gameRepository.GetGame(turn.GameId);
            var player = _playerRepository.GetPlayer(playerConnectionId);

            if (game.CurrentTurnPlayerId != playerConnectionId)
            {
                //Not this players turn, invalidate player
                _playerBusi.InvalidatePlayer(player.ConnectionId);
                return;
            }


            var validMove = _gameBoardBusi.AddTiles(turn.Placements);

            if(!validMove)
            {
                //Invalid move
                _playerBusi.InvalidatePlayer(player.ConnectionId);
            }

            var newTiles = game.TileBag.DrawTiles(turn.Placements.Count);

            throw new NotImplementedException();
        }

        public void SwapTiles(string playerConnectionId, SwapTilesTurnViewModel turn)
        {
            var game = _gameRepository.GetGame(turn.GameId);

			if (game.CurrentTurnPlayerId != playerConnectionId)
            {
                //Not this players turn, invalidate player
                _playerBusi.InvalidatePlayer(playerConnectionId);
                return;
            }

            //drawTiles if the player tries to draw more tiles than
            //there are in the bag That is an invalid move.
            if (game.TileBag.Count() < turn.TurnedInTiles.Count)
            {
				_playerBusi.InvalidatePlayer(playerConnectionId);
				return;
            }
            var newTiles = game.TileBag.DrawTiles(turn.TurnedInTiles.Count);
			var validMove = _playerBusi.RemoveTilesFromHand(turn.TurnedInTiles, playerConnectionId);
			if(!validMove)
			{
				//invalid turn. They are trying to turn in a tile that wasn't in their hand.
				_playerBusi.InvalidatePlayer(playerConnectionId);
			}

            game.TileBag.ReturnTiles(turn.TurnedInTiles);
            player.CurrentHand.AddRange(newTiles);
        }
    }
}
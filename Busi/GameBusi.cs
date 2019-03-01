using Busi.Helpers;
using Busi.IBusi;
using Busi.IRepo;
using Busi.IService;
using Models;
using Models.Enums;
using Models.EventModels;
using Models.ViewModels;
using System;
using System.Linq;

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
                _updater.UpdateClient(player.ConnectionId, game.GetViewModel(player.ConnectionId));
            }
        }

        public void AddPlayer(Guid gameId, Player player)
        {
            var game = _gameRepository.GetGame(gameId);
            game.Players.Add(player);
            _updater.UpdateGroup(game.GameId.ToString(), game.GetViewModel(null));
        }

        public void PlayTiles(string playerConnectionId, PlayTilesTurnViewModel turn)
        {
			//create a turn event which we will return to the group
			TurnEvent turnEvent = new TurnEvent(); //TODO what are we doing with this turn event?

            var game = _gameRepository.GetGame(turn.GameId);

            if (game.CurrentTurnPlayerId != playerConnectionId)
            {
                //Not this players turn, invalidate player
                _playerBusi.InvalidatePlayer(playerConnectionId);
                return;
            }

            var validMove = _playerBusi.RemoveTilesFromHand(turn.Placements.Select(p => p.Tile).ToList(), playerConnectionId);
            if (!validMove)
            {
                //invalid turn. They are trying to play a tile that wasn't in their hand.
                _playerBusi.InvalidatePlayer(playerConnectionId);
            }

            validMove = game.GameBoard.AddTiles(turn.Placements);
            //TODO get score for tile placements, maybe the return type of AddTiles should be the score?

            if(!validMove)
            {
                //Invalid move
                _playerBusi.InvalidatePlayer(playerConnectionId);
            }

            var newTiles = game.TileBag.DrawTiles(turn.Placements.Count);
            _playerBusi.AddTilesToHand(newTiles, playerConnectionId);
            //_updater.UpdateGroupTurnPlayed(game.GameId.ToString(),);
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
            _playerBusi.AddTilesToHand(newTiles, playerConnectionId);
        }
    }
}
using Busi.Helpers;
using Busi.IBusi;
using Busi.IRepo;
using Busi.IService;
using Models;
using Models.Enums;
using Models.EventModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
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

            var turnEvent = new TurnEvent
            {
                CurrentPlayerId = game.CurrentTurnPlayerId,
                PreviousPlayerId = null,
                TilesPlayed = null,
                Score = 0,
                RemovePlayer = false,
                SwappedTiles = 0
            };

            _updater.UpdateGroupTurnEvent(game.GameId.ToString(), turnEvent);
        }

        public void AddPlayer(Guid gameId, Player player)
        {
            var game = _gameRepository.GetGame(gameId);
            game.Players.Add(player);
            //_updater.UpdateGroup(game.GameId.ToString(), game.GetViewModel(null)); //TODO update group player joined
        }

        public void PlayTiles(string playerConnectionId, PlayTilesTurnViewModel turn)
        {
            var game = _gameRepository.GetGame(turn.GameId);

            if (game.CurrentTurnPlayerId != playerConnectionId)
            {
                //Not this players turn, invalidate player
                _playerBusi.InvalidatePlayer(playerConnectionId);
                //TODO Check for end of game
                //TODO Send infoEvent for invalidatingPlayer out of turn
                return;
            }

            var validMove = _playerBusi.RemoveTilesFromHand(turn.Placements.Select(p => p.Tile).ToList(), playerConnectionId);
            if (!validMove)
            {
                //invalid turn. They are trying to play a tile that wasn't in their hand.
                _playerBusi.InvalidatePlayer(playerConnectionId);
            }

            var score = game.GameBoard.AddTiles(turn.Placements);
            if (score == -1)
            {
                //Invalid move. Player banned.
                _playerBusi.InvalidatePlayer(playerConnectionId);
                //TODO Check for end of game
                StartNextTurn(game, 0, true, null, 0);
                return;
            }

			_playerBusi.AddScore(score, playerConnectionId);

            var newTiles = game.TileBag.DrawTiles(turn.Placements.Count);
            _playerBusi.AddTilesToHand(newTiles, playerConnectionId);

            //Send player their new hand
            _updater.UpdateClientEndTurnEvent(playerConnectionId, new EndTurnEvent { NewTiles = newTiles});

            StartNextTurn(game, score, !validMove, turn.Placements, 0);
        }

        public void SwapTiles(string playerConnectionId, SwapTilesTurnViewModel turn)
        {
            var game = _gameRepository.GetGame(turn.GameId);

			if (game.CurrentTurnPlayerId != playerConnectionId)
            {
                //Not this players turn, invalidate player
                //TODO Check for end of game
                //TODO Send infoEvent for invalidatingPlayer out of turn
                _playerBusi.InvalidatePlayer(playerConnectionId);
                return;
            }

            //drawTiles if the player tries to draw more tiles than
            //there are in the bag That is an invalid move.
            if (game.TileBag.Count() < turn.TurnedInTiles.Count)
            {
				_playerBusi.InvalidatePlayer(playerConnectionId);
                StartNextTurn(game, 0, true, null, 0);
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

            //Send player their new hand
            _updater.UpdateClientEndTurnEvent(playerConnectionId, new EndTurnEvent { NewTiles = newTiles });

            StartNextTurn(game, 0, false, null, turn.TurnedInTiles.Count);
        }

        private void StartNextTurn(Game game, int score, bool removePlayer, List<TilePlacement> tilesPlayed, int tilesSwapped)
        {
            var currentPlayerConnectionId = game.CurrentTurnPlayerId;

            //Figure out who goes next
            game.NextPlayersTurn();

            //Update group of turn
            var turnEvent = new TurnEvent
            {
                CurrentPlayerId = game.CurrentTurnPlayerId,
                PreviousPlayerId = currentPlayerConnectionId,
                TilesPlayed = tilesPlayed,
                Score = score,
                RemovePlayer = removePlayer,
                SwappedTiles = tilesSwapped
            };
            _updater.UpdateGroupTurnEvent(game.GameId.ToString(), turnEvent);
        }
    }
}
﻿using Busi.Helpers;
using Busi.IBusi;
using Busi.IRepo;
using Models;
using Models.Enums;
using Models.EventModels;
using Models.ClientCommands;
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
        private readonly IUpdater _updater;
		private readonly IPlayerBusi _playerBusi;

        public GameBusi(IGameRepository gameRepository, IPlayerRepository playerRepository, IShuffleHelper shuffleHelper, IUpdater updater, IPlayerBusi playerBusi)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _shuffleHelper = shuffleHelper;
            _updater = updater;
            _playerBusi = playerBusi;
        }

        public Game CreateGame(GameSettings settings)
        {
            return _gameRepository.CreateGame(settings);
        }

        public List<Game> GetLobbies()
        {
            return _gameRepository.GetLobbies();
        }

        public void StartGame(Guid gameId)
        {
            var game = _gameRepository.GetGame(gameId);
            game.Status = GameStatus.InProgress;
            _shuffleHelper.Shuffle(game.Players);
            game.CurrentTurnPlayerId = game.Players[0].ConnectionId;

            var startGameEvent = new StartGameEvent
            {
                CurrentPlayerId = game.CurrentTurnPlayerId,
                PlayerOrder = game.Players.Select(p => p.ConnectionId).ToList()
            };

            _updater.StartGameEvent(game.GameId.ToString(), startGameEvent);
        }

        public void AddPlayer(Guid gameId, string playerConnectionId)
        {
            var player = _playerRepository.GetPlayer(playerConnectionId);
            var playerJoinedEvent = new PlayerJoinedEvent
			{
				Name = player.Name,
				PlayerId = player.ConnectionId
			};
            _updater.PlayerJoinedGameLobbyEvent(gameId.ToString(), playerJoinedEvent);
            var game = _gameRepository.GetGame(gameId);
            game.Players.Add(player);
            var players = game.Players.Select(p => (p.ConnectionId, p.Name)).ToList();
            var gameInfoEvent = new GameInfoEvent
            {
                Players = players,
                GameSettings = game.GameSettings
            };
            _updater.GameInfoEvent(player.ConnectionId, gameInfoEvent);
        }

        public void PlayTiles(string playerConnectionId, PlayTilesTurn turn)
        {
            var game = _gameRepository.GetGame(turn.GameId);

            if (game.CurrentTurnPlayerId != playerConnectionId)
            {
                //Not this players turn, invalidate player
                _playerBusi.InvalidatePlayer(playerConnectionId, game.GameId.ToString());
				IsEndOfGame(game);
                return;
            }

            var validMove = _playerBusi.RemoveTilesFromHand(turn.Placements.Select(p => p.Tile).ToList(), playerConnectionId);
            if (!validMove)
            {
                //invalid turn. They are trying to play a tile that wasn't in their hand.
                _playerBusi.InvalidatePlayer(playerConnectionId, game.GameId.ToString());
                IsEndOfGame(game);
            }

            var score = game.GameBoard.AddTiles(turn.Placements);
            if (score == -1)
            {
                //Invalid move. Player banned.
                _playerBusi.InvalidatePlayer(playerConnectionId, game.GameId.ToString());
                if (IsEndOfGame(game))
                    return;
                StartNextTurn(game, 0, true, null, 0);
                return;
            }

			_playerBusi.AddScore(score, playerConnectionId);

            var newTiles = game.TileBag.DrawTiles(turn.Placements.Count);
            _playerBusi.AddTilesToHand(newTiles, playerConnectionId);

            if (IsEndOfGame(game))
            {
                return;
            }

            //Send player their new hand
            _updater.UpdateClientEndTurnEvent(playerConnectionId, new EndTurnEvent { NewTiles = newTiles});

            StartNextTurn(game, score, !validMove, turn.Placements, 0);
        }

        public void SwapTiles(string playerConnectionId, SwapTilesTurn turn)
        {
            var game = _gameRepository.GetGame(turn.GameId);

			if (game.CurrentTurnPlayerId != playerConnectionId)
            {
                //Not this players turn, invalidate player
                IsEndOfGame(game);
                _playerBusi.InvalidatePlayer(playerConnectionId, game.GameId.ToString());
                return;
            }

            //drawTiles if the player tries to draw more tiles than
            //there are in the bag That is an invalid move.
            if (game.TileBag.Count() < turn.TurnedInTiles.Count)
            {
				_playerBusi.InvalidatePlayer(playerConnectionId, game.GameId.ToString());
                if (IsEndOfGame(game))
                    return;
                StartNextTurn(game, 0, true, null, 0);
                return;
            }
            var newTiles = game.TileBag.DrawTiles(turn.TurnedInTiles.Count);
			var validMove = _playerBusi.RemoveTilesFromHand(turn.TurnedInTiles, playerConnectionId);
			if(!validMove)
			{
				//invalid turn. They are trying to turn in a tile that wasn't in their hand.
				_playerBusi.InvalidatePlayer(playerConnectionId, game.GameId.ToString());
                IsEndOfGame(game);
                return;
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

        private bool IsEndOfGame(Game game)
        {
            if (game.IsEndOfGame())
            {
                game.Status = GameStatus.GameEnded;
                //TODO create closeGame repo method that stores a replay in DB and cleans up memory (remove game from game repo)
                var endGameEvent = new EndGameEvent
                {
                    Scores = game.Players.Select(p => (p.Name, p.Score)).ToList()
                };
                _updater.EndGameEvent(game.GameId.ToString(),endGameEvent);
                return true;
            }
			return false;
        }
	}
}
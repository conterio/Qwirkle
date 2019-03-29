using Busi.IBusi;
using Busi.IRepo;
using Models;
using Models.EventModels;
using System.Collections.Generic;

namespace Busi
{
    public class PlayerBusi : IPlayerBusi
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUpdater _updater;

        public PlayerBusi(IPlayerRepository playerRepository, IUpdater updater)
        {
            _playerRepository = playerRepository;
            _updater = updater;
        }
        public void InvalidatePlayer(string connectionId, string groupId)
        {
            var player = _playerRepository.GetPlayer(connectionId);
            player.StillPlaying = false;
            _updater.PlayerRemoveEvent(groupId, new PlayerRemovedEvent()
            {
                CurrentPlayerId = connectionId
            });

        }

        /// <summary>
        /// Go through the player's current hand and remove tiles that were passed in.
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="playerConnectionId"></param>
        /// <returns>
        /// Returns false if you are trying to remove a tile that doesn't exist in the player's hand
        /// Returns true if all the tiles were successfully removed
        /// </returns>
		public bool RemoveTilesFromHand(List<Tile> tiles, string playerConnectionId)
        {
            var player = _playerRepository.GetPlayer(playerConnectionId);

            foreach (var tile in tiles)
            {
                if (!player.CurrentHand.Contains(tile))
                {
                    return false;
                }

                player.CurrentHand.Remove(tile);
            }
            return true;
        }

        public void AddTilesToHand(List<Tile> tiles, string playerConnectionId)
        {
            var player = _playerRepository.GetPlayer(playerConnectionId);
            player.CurrentHand.AddRange(tiles);
        }

        public void AddScore(int score, string playerConnectionId)
        {
            var player = _playerRepository.GetPlayer(playerConnectionId);
            player.Score += score;
        }

        public void AddPlayer(string connectionId, string playerName, bool isHumanPlayer)
        {
            _playerRepository.AddPlayer(connectionId, playerName, isHumanPlayer);
        }

        public List<Player> GetAllPlayers()
        {
            return _playerRepository.GetAllPlayers();
        }

        public Player GetPlayer(string connectionId)
        {
            return _playerRepository.GetPlayer(connectionId);
        }
    }
}
using Busi.IBusi;
using System;
using System.Collections.Generic;
using System.Text;
using Busi.IRepo;
using Models;

namespace Busi
{
	public class PlayerBusi : IPlayerBusi
	{
		private readonly IPlayerRepository _playerRepository;

		public PlayerBusi(IPlayerRepository playerRepository)
        {
			_playerRepository = playerRepository;
		}
		public void InvalidatePlayer(string connectionId)
		{
            var player = _playerRepository.GetPlayer(connectionId);
            player.StillPlaying = false;
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
	}
}
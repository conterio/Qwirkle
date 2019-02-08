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

        public bool
	}
}
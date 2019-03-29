using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ClientOutBound
{
	public class PlayerViewModel
	{
		public PlayerViewModel(Player player)
		{
			Id = player.ConnectionId;
			Name = player.Name;
			IsHumanPlayer = player.IsHumanPlayer;
			IsSpectator = player.IsSpectator;
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsHumanPlayer { get; set; }
		public bool IsSpectator { get; set; }
	}
}

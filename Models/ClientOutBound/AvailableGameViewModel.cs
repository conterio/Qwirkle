using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ClientOutBound
{
	public class AvailableGameViewModel
	{
		public AvailableGameViewModel(Game game)
		{
			GameId = game.GameId;
			GameName = game.GameName;
			GameSettings = game.GameSettings;
			AvaiableSpots = game.GameSettings.MaxPlayers - game.Players.Count;
		}

		public Guid GameId { get; set; }
		public string GameName { get; set; }
		public GameSettings GameSettings { get; set; }
		public int AvaiableSpots { get; set; }


	}
}

using Models.Enums;
using System;
using System.Collections.Generic;

namespace Models.ViewModels
{
	public class TurnPlayedViewModel
	{
		public PlayerViewModel Player { get; set;}
		public TilePlacement TilePlacement { get; set; }
	}
}
using Models.Enums;
using System;
using System.Collections.Generic;

namespace Models.ViewModels
{
    public class GameViewModel
    {
        public Guid GameId { get; set; }
        public List<PlayerViewModel> Players { get; set; }
        public GameSettings GameSettings { get; set; }
        public GameStatus GameStatus { get; set; }
        public string CurrentTurnPlayerId { get; set; }
        public int NumberOfTilesInBag { get; set; }
    }
}
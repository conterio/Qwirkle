using System;
using System.Collections.Generic;
using System.Text;
using Models.Enums;

namespace Models.ViewModels
{
    public class GameViewModel
    {
        public Guid GameId { get; set; }
        public List<PlayerViewModel> Players { get; set; }
        public GameSettings GameSettings { get; set; }
        public GameStatus GameStatus { get; set; }
    }
}

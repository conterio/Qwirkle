using System;
using System.Collections.Generic;
using System.Linq;
using Models.Enums;
using Models.Interfaces;
using Models.ViewModels;

namespace Models
{
    public class Game
    {
        public Game()
        {
            Players = new List<IPlayer>();
            Turns = new List<ITurn>();
        }

        public Guid GameId { get; set; }
        public List<IPlayer> Players { get; set; }
        public List<ITurn> Turns { get; set; }
        public GameSettings GameSettings { get; set; }
        public GameStatus Status { get; set; }

        public GameViewModel GetViewModel()
        {
            return new GameViewModel()
            {
                GameId = this.GameId,
                Players = new List<PlayerViewModel>(this.Players.Select(p => p.GetViewModel()).ToList()),
                GameSettings = this.GameSettings,
                GameStatus = this.Status
            };
        }
    }
}
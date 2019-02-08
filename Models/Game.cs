using Models.Enums;
using Models.ViewModels;
using Qwirkle.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Game
    {
        public Game()
        {
            Players = new List<Player>();
            Turns = new List<ITurn>();
            TileBag = new TileBag();
        }

        public Guid GameId { get; set; }
        public List<Player> Players { get; set; }
        public List<ITurn> Turns { get; set; }
        public GameSettings GameSettings { get; set; }
        public GameStatus Status { get; set; }
        public TileBag TileBag { get; set; }
        public string CurrentTurnPlayerId { get; set; }
        public GameBoard GameBoard { get; set;}
        
		public GameViewModel GetViewModel(string playerConnectionId)
        {
            return new GameViewModel()
            {
                GameId = this.GameId,
                Players = new List<PlayerViewModel>(this.Players.Select(p => p.GetViewModel(playerConnectionId == p.ConnectionId)).ToList()),
                GameSettings = this.GameSettings,
                GameStatus = this.Status,
                CurrentTurnPlayerId = CurrentTurnPlayerId,
                NumberOfTilesInBag = TileBag.Count()
            };
        }
    }
}
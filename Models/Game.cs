using Models.Enums;
using Models.ClientCommands;
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
            Status = GameStatus.Lobby;
            GameSettings = new GameSettings();
            TileBag = new TileBag();
            GameBoard = new GameBoard();
        }

        public Guid GameId { get; set; }
        public List<Player> Players { get; set; }
        public List<ITurn> Turns { get; set; }
        public GameSettings GameSettings { get; set; }
        public GameStatus Status { get; set; }
        public TileBag TileBag { get; set; }
        public string CurrentTurnPlayerId { get; set; }
        public GameBoard GameBoard { get; set;}

        public void NextPlayersTurn()
        {
            if (Players.Count(p => p.StillPlaying) < 2)
            {
                //Only 1 player playing
                return;
            }
            var player = Players.SingleOrDefault(p => p.ConnectionId == CurrentTurnPlayerId);
            var index = Players.IndexOf(player);
            Player nextPlayer;
            do
            {
                if (index == Players.Count)
                {
                    index = 0;
                }
                else
                {
                    ++index;
                }

                nextPlayer = Players[index];
                CurrentTurnPlayerId = nextPlayer.ConnectionId;
            } while (!nextPlayer.StillPlaying);
        }

        public bool IsEndOfGame()
        {
            if (Players.Count(p => p.StillPlaying) < 2)
            {
                //Only 1 player playing
                return true;
            }

            if (TileBag.Count() > 0)
            {
                //Still tiles left in bad
                return false;
            }

            //Tile bag empty and player plays all tiles in hand
            var player = Players.SingleOrDefault(p => p.ConnectionId == CurrentTurnPlayerId && p.StillPlaying);
            return player?.CurrentHand.Count == 0;
        }
    }
}
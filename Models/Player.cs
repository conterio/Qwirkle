using Models.Interfaces;
using Models.ViewModels;

namespace Models
{
    public class Player : IPlayer
    {
        private const int MAX_HAND_SIZE = 6;

        public Player()
        {
            this.CurrentHand = new Tile[MAX_HAND_SIZE];
        }
        public int Score { get; set; }
        public Tile[] CurrentHand { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public bool IsHumanPlayer { get; set; }

        public PlayerViewModel GetViewModel()
        {
            return new PlayerViewModel()
            {
                Score = this.Score,
                Name = this.Name,
                IsHumanPlayer = this.IsHumanPlayer
            };
        }
    }
}
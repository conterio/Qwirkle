namespace Models.Interfaces
{
    public interface IPlayer
    {
        string ConnectionId { get; set; }
        string Name { get; set; }
        bool IsHumanPlayer { get; set; }
    }
}

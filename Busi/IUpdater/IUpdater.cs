namespace Busi.IUpdater
{
    /// <summary>
    /// IUpdater is an interface for outgoing calls. All return types must be void
    /// </summary>
    public interface IUpdater
    {
        void UpdateGroupTurnEvent(string groupId, object payload);
        void UpdateClientEndTurnEvent(string connectionId, object payload);
    }
}
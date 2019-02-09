namespace Busi.IUpdater
{
    /// <summary>
    /// IUpdater is an interface for outgoing calls. All return types must be void
    /// </summary>
    public interface IUpdater
    {
        void UpdateGroup(string groupId,object payload);
        void UpdateClient(string connectionId, object payload);
        void UpdateGroupTurnPlayed(string groupId, object payload);
    }
}
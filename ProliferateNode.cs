using System.Collections.Generic;

public class ProliferateNode : ProliferateManipulable
{
    #region fields
    public int NodeID { get; private set; }
    public List<int> NameRefs;
    public List<int> DepictionRefs;
    Dictionary<int, List<ProliferateEdge>> _edges;
    #endregion

    #region methods
    public ProliferateNode(int kindID) : base(kindID)
    {
        
    }
    public ProliferateEdge SpawnEdge(
        int kindID, 
        ProliferateNode target)
    {
        // code here
    }
    public void DespawnEdge(
        int kindID,
        ProliferateNode target)
    {
        // code here
    }
    #endregion
}
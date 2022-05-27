using System.Collections.Generic;

public class ProliferateHub
{
    // adds initial attributes and edges 
    public delegate void NodeSetupHandler(ProliferateNode node);

    #region fields
    static int latestKindID;
    static Dictionary<string, int> _kindIDs;
    public int Ticks { get; private set; }
    public ProliferateNamer Namer { get; private set; }
    public ProliferateDescriber Describer { get; private set; }
    public ProliferateDepicter Depicter { get; private set; }
    Dictionary<int, List<ProliferateNode>> _nodes;
    Dictionary<int, List<ProliferateEdge>> _edges;
    Dictionary<int, ProliferateBehaviour> _nodeBehaviours;
    Dictionary<int, ProliferateBehaviour> _edgeBehaviours;
    #endregion

    #region  methods
    static ProliferateHub()
    {
        latestKindID = -1;
        _kindIDs = new Dictionary<string, int>();
    }   
    public ProliferateHub(
        ProliferateNamer namer,
        ProliferateDescriber describer,
        ProliferateDepicter depicter,
        List<ProliferateBehaviour> nodeBehaviours,
        List<ProliferateBehaviour> edgeBehaviours)
    {
        Namer = namer;
        Describer = describer;
        Depicter = depicter;
        _nodeBehaviours = nodeBehaviours;
        _edgeBehaviours = edgeBehaviours;
    }
    public static int GetKindID(string kindTitle)
    {
        if(_kindIDs.TryGetValue(kindTitle, out int kindID))
            return kindID;
        else
        {
            latestKindID++;
            _kindIDs.Add(kindTitle, latestKindID);
            return latestKindID;
        }
    }
    public ProliferateNode SpawnNode(
        int kindID,
        NodeSetupHandler nodeSetupHandler,
        ProliferateBehaviour behaviour)
    {
        var node = new ProliferateNode(kindID);
        _nodes.Add(kindID, node);
        nodeSetupHandler?.Invoke(node);
        behaviour.RegisterManipulable(node);
        return node;
    }
    public void DespawnNode(
        ProliferateNode node,
        ProliferateBehaviour behaviour)
    {
        behaviour.DeregisterManipulable(node);
        if(_nodes.TryGetValue(node.KindID, out List<ProliferateNode> nodeList))
            nodeList.Remove(node);
    }
    public void Tick()
    {
        foreach (var nodeBehaviour in _nodeBehaviours.Values)
            nodeBehaviour.Tick();
        foreach (var edgeBehaviour in _edgeBehaviours.Values)
            edgeBehaviour.Tick();
    }
    #endregion
}
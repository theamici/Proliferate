using System.Collections.Generic;

public class ProliferateBehaviour
{
    public delegate void ManipulableHandler(ProliferateManipulable manipulable);

    #region fields
    public int KindID { get; private set; }
    ManipulableHandler _created;
    ManipulableHandler _timePassed;
    ManipulableHandler _terminated;
    List<ProliferateManipulable> _manipulables;
    #endregion

    public ProliferateBehaviour(
        int kindID,
        ManipulableHandler created,
        ManipulableHandler timePassed,
        ManipulableHandler terminated)
    {
        KindID = kindID;
        _created = created;
        _timePassed = timePassed;
        _terminated = terminated;
        _manipulables = new List<ProliferateManipulable>();
    }

    #region methods
    public void RegisterManipulable(ProliferateManipulable manipulable)
    {
        if(!_manipulables.Contains(manipulable))
            _manipulables.Add(manipulable);

        _created?.Invoke(manipulable);
    }
    public void DeregisterManipulable(ProliferateManipulable manipulable)
    {
        _manipulables.Remove(manipulable);
        _terminated?.Invoke(manipulable);
    }
    public void Tick()
    {
        foreach (var m in _manipulables)
        {
            _timePassed?.Invoke(m, null); 
        }
    }
    #endregion
}
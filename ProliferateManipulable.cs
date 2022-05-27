using System.Collections.Generic;

public abstract class ProliferateManipulable : ScriptableObject
{
    public int KindID { get; private set; }
    Dictionary<int, int> _discreteAttributes;
    Dictionary<int, float> _continuousAttributes;

    #region methods
    public ProliferateManipulable(int kindID)
    {
        KindID = kindID;
        _constantAttributes = new Dictionary<int, int>();
        _discreteAttributes = new Dictionary<int, int>();
        _continuousAttributes = new Dictionary<int, float>();
    }
    public void ModifyDiscrete(int attributeID, int change)
    {
        if(_discreteAttributes.ContainsKey(attributeID))
            _discreteAttributes[attributeID] += change;
        else
            _discreteAttributes.Add(attributeID, change);
    }
    public void ModifyContinuous(int attributeID, float change)
    {
        if(_continuousAttributes.ContainsKey(attributeID))
            _continuousAttributes[attributeID] += change;
        else
            _continuousAttributes.Add(attributeID, change);
    }
    public int GetDiscrete(int attributeID)
    {
        _discretetAttributes.TryGetValue(attributeID, out int value);
        return value;
    }
    public int GetContinuous(int attributeID)
    {
        _continuousAttributes.TryGetValue(attributeID, out float value);
        return value;
    }
    #endregion
}
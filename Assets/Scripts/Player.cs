using UnityEngine;

public class Player : MonoBehaviour
{
    // External variables
    [SerializeField] private Board _board;
    [SerializeField] private Manager _manager;

    // Variables
    public bool Automated;

    public int Score
    {
        get { return Marker == Marker.Cross ? _manager.XScore : _manager.OScore; }
        set
        {
            if (Marker == Marker.Cross)
                _manager.XScore = value;
            else
                _manager.OScore = value;
        }
    }
    
    public Marker Marker;

    public void AutoMove()
    {
        
    }
}
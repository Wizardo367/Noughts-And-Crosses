using UnityEngine;
using UnityEngine.UI;

public class BoardSpace : MonoBehaviour
{
    // Variables
    public bool Occupied;
    public Marker Marker;

    private Animator _animator;
    private Image _image;
    
    private void Start()
    {
        // Get components
        _animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
    }

    public void Clear()
    {
        // Clear board space
        Occupied = false;
        Marker = Marker.None;

        // Set animation
        _animator.SetBool("markerClear", true);
    }

    public bool PlaceMarker(Marker marker)
    {
        if (marker != Marker.None)
        {
            // Set sprite
            _image.sprite = marker == Marker.Cross ? Board.CrossSprite : Board.NoughtSprite;
            // Set colour
            _image.color = Color.white;
            // Set space as occupied
            Occupied = true;
            // Set animation
            _animator.SetBool("markerPlace", true);

            return true;
        }

        return false;
    }

    public void ResetAnimationVariables()
    {
        _animator.SetBool("markerPlace", false);
        _animator.SetBool("markerClear", false);
    }
}
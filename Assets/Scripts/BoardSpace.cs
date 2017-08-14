using UnityEngine;
using UnityEngine.UI;

public class BoardSpace : MonoBehaviour
{
    // Variables
    public bool Occupied;
    public Marker Marker;

    private Animator _animator;
    private Image _image;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _placementSound;

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
        // Check if any animations are playing or marker type is none
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("BoardSpaceDefault") ||
            marker == Marker.None)
            return false;

        // Set marker type
        Marker = marker;
        // Set sprite
        _image.sprite = Marker == Marker.Cross ? Board.CrossSprite : Board.NoughtSprite;
        // Set colour
        _image.color = Color.white;
        // Set space as occupied
        Occupied = true;
        // Set animation
        _animator.SetBool("markerPlace", true);
        // Play sound effect
        _audioSource.clip = _placementSound;
        _audioSource.Play();

        return true;
    }

    public void ResetAnimationVariables()
    {
        _animator.SetBool("markerPlace", false);
        _animator.SetBool("markerClear", false);
    }
}
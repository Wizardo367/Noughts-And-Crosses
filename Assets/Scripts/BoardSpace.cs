using UnityEngine;
using UnityEngine.UI;

public class BoardSpace : MonoBehaviour
{
    // Variables
    public bool Occupied;

    private Image _image;

    private void Start()
    {
        // Get components
        _image = GetComponent<Image>();
    }

    public void Clear()
    {
        // Clear board space
        _image.sprite = null;
        _image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }
}
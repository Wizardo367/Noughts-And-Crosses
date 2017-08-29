using System.Linq;
using UnityEngine;

public enum Marker { None, Cross, Nought }

public class Board : MonoBehaviour
{
    // Variables
    [SerializeField]
    private Manager _manager;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _clearBoardSound;

    private BoardSpace[] _boardSpaces = new BoardSpace[9];

    public static Sprite CrossSprite, NoughtSprite;
    public Marker[] Spaces = new Marker[9];

    private void Start()
    {
        // Get assets
        CrossSprite = Resources.Load<Sprite>("Images/cross");
        NoughtSprite = Resources.Load<Sprite>("Images/nought");

        // Get board spaces
        int childCounter = 0;
        foreach (Transform child in transform)
        {
            _boardSpaces[childCounter] = child.GetComponent<BoardSpace>();
            childCounter++;
        }
    }

    public void PlaceMarker(int position)
    {
        BoardSpace currentBoardSpace = _boardSpaces[position];

        /* Check if the game is already over
           Check if space is occupied
           Check if the clear animation has completed */
        if (_manager.GameOver || currentBoardSpace.Occupied)
            return;

        // Place marker
        Marker currentPlayerMarker = _manager.CurrentPlayer.Marker;

        // Set space
        if (currentBoardSpace.PlaceMarker(currentPlayerMarker))
            Spaces[position] = currentPlayerMarker;
        else
            return;

        // Check for winner
        Evaluate();

        // Swap player
        _manager.SwapPlayer();
    }

    public void Clear()
    {
        // Clear board spaces
        foreach (BoardSpace space in _boardSpaces)
        {
            space.Occupied = false;
            space.Clear();
        }

        // Reset spaces
        Spaces = new Marker[9];

        // Play sound effect
        _audioSource.clip = _clearBoardSound;
        _audioSource.Play();
    }

    public void Evaluate()
    {
        // Check game state

        // Horizontal
        for (int i = 0; i < 7; i+=3)
            if ((Spaces[i] == Marker.Cross || Spaces[i] == Marker.Nought) &&
                Spaces[i] == Spaces[i + 1] && Spaces[i + 1] == Spaces[i + 2])
            {
                GameOver(Spaces[i] + " Wins!");
                return;
            }

        // Vertical
        for (int i = 0; i < 3; i++)
            if ((Spaces[i] == Marker.Cross || Spaces[i] == Marker.Nought) &&
                Spaces[i] == Spaces[i + 3] && Spaces[i + 3] == Spaces[i + 6])
            {
                GameOver(Spaces[i] + " Wins!");
                return;
            }

        // Diagonal
        if ((Spaces[4] == Marker.Cross || Spaces[4] == Marker.Nought) &&
            (Spaces[0] == Spaces[4] && Spaces[4] == Spaces[8] ||
            Spaces[2] == Spaces[4] && Spaces[4] == Spaces[6]))
        {
            GameOver(Spaces[4] + " Wins!");
            return;
        }

        // Check for draw
        int occupiedCounter = Spaces.Count(marker => marker == Marker.Cross || marker == Marker.Nought);

        if (occupiedCounter == 9)
            GameOver("Tie");
    }

    private void GameOver(string msg)
    {
        _manager.GameStatus = msg;
        _manager.GameOver = true;
    }
}

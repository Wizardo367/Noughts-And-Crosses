using UnityEngine;
using UnityEngine.UI;

public enum Marker { None, Cross, Nought }

public class Board : MonoBehaviour
{
    // Variables
    [SerializeField]
    private Manager _manager;

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
           Check if space is occupied */
        if (_manager.GameOver || currentBoardSpace.Occupied)
            return;

        // Place marker
        Marker currentPlayer = _manager.CurrentPlayer;

        // Set space
        if (currentBoardSpace.PlaceMarker(currentPlayer))
            Spaces[position] = currentPlayer;

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
    }

    public void Evaluate()
    {
        // Check game state

        // Check for win

        // TODO Fix mess

        // Horizontal
        for (int i = 0; i < 7; i+=3)
            if ((Spaces[i] == Marker.Cross || Spaces[i] == Marker.Nought) &&
                Spaces[i] == Spaces[i + 1] && Spaces[i + 1] == Spaces[i + 2])
            {
                _manager.GameStatus = Spaces[i] + " Wins!";
                _manager.GameOver = true;
                return;
            }

        // Vertical
        for (int i = 0; i < 3; i++)
            if ((Spaces[i] == Marker.Cross || Spaces[i] == Marker.Nought) &&
                Spaces[i] == Spaces[i + 3] && Spaces[i + 3] == Spaces[i + 6])
            {
                _manager.GameStatus = Spaces[i] + " Wins!";
                _manager.GameOver = true;
                return;
            }

        // Diagonal
        if ((Spaces[4] == Marker.Cross || Spaces[4] == Marker.Nought) &&
            (Spaces[0] == Spaces[4] && Spaces[4] == Spaces[8] ||
            Spaces[2] == Spaces[4] && Spaces[4] == Spaces[6]))
        {
            _manager.GameStatus = Spaces[4] + " Wins!";
            _manager.GameOver = true;
            return;
        }

        // Check for draw
        int occupiedCounter = 0;

        for (int i = 0; i < 9; i++)
            if (Spaces[i] == Marker.Cross || Spaces[i] == Marker.Nought)
                occupiedCounter++;

        if (occupiedCounter == 9)
        {
            _manager.GameStatus = "Tie";
            _manager.GameOver = true;
        }
    }
}

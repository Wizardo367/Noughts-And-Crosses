using UnityEngine;
using UnityEngine.UI;

public enum GameMode { Single, TwoPlayer }

public class Manager : MonoBehaviour
{
    // External variables
    [SerializeField] private Text _xScoreText;
    [SerializeField] private Text _oScoreText;
    [SerializeField] private Text _currentPlayerText;
    [SerializeField] private Text _gameStatusText;

    // Game Variables
    private Board _board;

    private readonly Color _crossColour = new Color(0f, 114/255f, 188/255f);
    private readonly Color _noughtColour = new Color(211/255f, 53/255f, 53/255f);

    public int XScore, OScore;
    public Marker CurrentPlayer { get; private set; }

    private string _gameStatus;
    public string GameStatus
    {
        get { return _gameStatus; }
        set
        {
            // Set variable
            _gameStatus = value;
            // Update text
            _gameStatusText.text = _gameStatus;
            // Display text
            _gameStatusText.gameObject.SetActive(true);
        }
    }

    public GameMode GameType = GameMode.Single;

    private void Start()
    {
        // Set variables
        CurrentPlayer = Marker.Cross;
        _board = GameObject.Find("Board").GetComponent<Board>();
    }

    /// <summary>
    /// Resets the board or game.
    /// </summary>
    public void Wipe(bool resetAll = false)
    {
        // Reset variables
        _gameStatusText.enabled = false;
        CurrentPlayer = CurrentPlayer == Marker.Cross ? Marker.Nought : Marker.Cross;

        // Check if everything needs to be reset
        if (resetAll)
        {
            XScore = 0;
            OScore = 0;
        }

        // Reset board
        _board.Clear();
    }

    /// <summary>
    /// Starts the next game.
    /// </summary>
    public void NextGame()
    {
        // Add to score


        // Reset game properties
        Wipe();
    }

    /// <summary>
    /// Starts a new game. (Wipes all previous information)
    /// </summary>
    /// <param name="singlePlayer"></param>
    public void NewGame(bool singlePlayer = true)
    {
        // Check game mode
        GameType = !singlePlayer ? GameMode.TwoPlayer : GameMode.Single;

        // Reset game manager
        Wipe(true);
    }

    public void SwapPlayer()
    {
        // Update variable and text
        if (CurrentPlayer == Marker.Cross)
        {
            CurrentPlayer = Marker.Nought;
            _currentPlayerText.color = _noughtColour;
            _currentPlayerText.text = "Noughts";
        }
        else
        {
            CurrentPlayer = Marker.Cross;
            _currentPlayerText.color = _crossColour;
            _currentPlayerText.text = "Crosses";
        }
    }
}
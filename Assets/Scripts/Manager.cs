using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameMode { Single, TwoPlayer }

public class Manager : MonoBehaviour
{
    // External variables
    [SerializeField] private Player _player1;
    [SerializeField] private Player _player2;

    [SerializeField] private Text _xScoreText;
    [SerializeField] private Text _oScoreText;
    [SerializeField] private Text _currentPlayerText;
    [SerializeField] private Text _gameStatusText;

    [SerializeField] private Button _audioButton;

    private AudioSource _audioSource;

    // Game Variables
    private Board _board;

    private readonly Color _crossColour = new Color(0f, 114/255f, 188/255f);
    private readonly Color _noughtColour = new Color(211/255f, 53/255f, 53/255f);

    public bool GameOver; 
    public static GameMode GameType = GameMode.Single;

    private int _xScore;
    public int XScore
    {
        get { return _xScore; }
        set
        {
            // Update score and text
            _xScore = value;
            _xScoreText.text = _xScore.ToString();
        }
    }

    private int _oScore;
    public int OScore {
        get { return _oScore; }
        set
        {
            _oScore = value;
            _oScoreText.text = _oScore.ToString();
        }
    }

    public Player CurrentPlayer { get; private set; }

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
            // Enable variable
            _gameStatusText.enabled = true;
            // Display text
            _gameStatusText.gameObject.SetActive(true);

            // Update score
            char winner = _gameStatus[0];

            if (winner == 'C')
                XScore++;
            else if (winner == 'N')
                OScore++;

            // Next game
            Invoke("NextGame", 2f);
        }
    }

    private void Start()
    {
        // Set variables
        Marker startingMarker = Random.Range(0f, 1f) < 0.5f ? Marker.Cross : Marker.Nought;
        _player1.Marker = startingMarker;
        _player2.Marker = _player1.Marker == Marker.Cross ? Marker.Nought : Marker.Cross;
        CurrentPlayer = _player2;
        SwapPlayer();

        // Get components
        _audioSource = GetComponent<AudioSource>();
        _board = GameObject.Find("Board").GetComponent<Board>();
    }

    /// <summary>
    /// Resets the board or game.
    /// </summary>
    public void Wipe(bool resetAll = false)
    {
        // Reset variables
        GameOver = false;
        _gameStatusText.enabled = false;
        SwapPlayer();

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
        // Reset game properties
        Wipe();
    }

    /// <summary>
    /// Starts a new game. (Wipes all previous information)
    /// </summary>
    /// <param name="singlePlayer"></param>
    public void NewGame(bool singlePlayer = true)
    {
        // Reset game manager
        Wipe(true);
    }

    public void SwapPlayer()
    {
        // Update variable and text
        if (CurrentPlayer.Marker == Marker.Cross)
        {
            _currentPlayerText.color = _noughtColour;
            _currentPlayerText.text = "Noughts";
        }
        else
        {
            _currentPlayerText.color = _crossColour;
            _currentPlayerText.text = "Crosses";
        }

        // Swap players
        CurrentPlayer = CurrentPlayer.gameObject.name == "Player1" ? _player2 : _player1;

        // Check if new player is automated, if so, make a move
        if (CurrentPlayer.Automated)
            CurrentPlayer.AutoMove();
    }

    public void ToggleAudio()
    {
        // Mute/unmute audio source
        bool muted = !_audioSource.mute;
        _audioSource.mute = muted;

        // Toggle button colour
        _audioButton.image.color = muted ? Color.grey : Color.white;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
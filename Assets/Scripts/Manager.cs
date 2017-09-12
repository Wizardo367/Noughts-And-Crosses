using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameMode { SinglePlayer, TwoPlayer }

public class Manager : MonoBehaviour
{
    // External variables
    public Player Player1;
    public Player Player2;

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
    public static GameMode GameMode = GameMode.SinglePlayer;

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

            // Next game
            Invoke("NextGame", 2f);
        }
    }

    private void Start()
    {
        // Check game mode
        Player2.Automated = GameMode == GameMode.SinglePlayer;

        // Set variables
        Marker startingMarker = UnityEngine.Random.Range(0f, 1f) < 0.5f ? Marker.Cross : Marker.Nought;
        Player1.Marker = startingMarker;
        Player2.Marker = Player1.Marker == Marker.Cross ? Marker.Nought : Marker.Cross;
        System.Random random = new System.Random();
        CurrentPlayer = random.Next(0, 2) == 0 ? Player1 : Player2;

        // Get components
        _audioSource = GetComponent<AudioSource>();
        _board = GameObject.Find("Board").GetComponent<Board>();

        // Swap player
        Invoke("SwapPlayer", 0.1f); // Delay prevents NullReferenceException
    }

    /// <summary>
    /// Resets the board or game.
    /// </summary>
    public void Wipe(bool resetAll = false)
    {
        // Reset variables
        _gameStatusText.enabled = false;

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
        // Swap players
        Invoke("SwapPlayer", 1f); // Delay prevents error
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
        CurrentPlayer = CurrentPlayer.gameObject.name == "Player1" ? Player2 : Player1;

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
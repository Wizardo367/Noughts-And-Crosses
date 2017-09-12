using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    // --- Methods

    /// <summary>
    /// Starts the game.
    /// </summary>
    /// <param name="singlePlayer">if set to <c>true</c> [single player].</param>
    public void StartGame(bool singlePlayer)
    {
        // Set game type
        Manager.GameMode = singlePlayer ? GameMode.SinglePlayer : GameMode.TwoPlayer;
        // Load scene
        SceneManager.LoadScene("Game");
    }
}
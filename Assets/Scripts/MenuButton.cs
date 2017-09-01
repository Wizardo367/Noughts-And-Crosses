using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void StartGame(bool singlePlayer)
    {
        // Set game type
        Manager.GameMode = singlePlayer ? GameMode.SinglePlayer : GameMode.TwoPlayer;
        // Load scene
        SceneManager.LoadScene("Game");
    }
}
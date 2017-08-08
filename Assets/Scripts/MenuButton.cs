using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void StartGame(bool singlePlayer)
    {
        // Set game type
        Manager.GameType = singlePlayer ? GameMode.Single : GameMode.TwoPlayer;
        // Load scene
        SceneManager.LoadScene("Game");
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        // Start the game
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}

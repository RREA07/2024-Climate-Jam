using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void quitGame()
    {
        Application.Quit();
    }

    public void gameStart()
    {
        SceneManager.LoadSceneAsync(1);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Quits game
    public void quitGame()
    {
        Application.Quit();
    }

    //Starts game
    public void gameStart()
    {
        SceneManager.LoadSceneAsync(3);
    }
}

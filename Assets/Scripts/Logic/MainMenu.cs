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

    public void gameReStart()
    {
        SceneManager.LoadScene("Forest_lvl_Latest");
    }
}

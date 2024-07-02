using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gamePauseMenu;
    private SoundFXManager soundFXManager;

    void Start()
    {
        soundFXManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundFXManager>();
    }

    //Loads game over
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        Cursor.visible = true;
        soundFXManager.playSFX(soundFXManager.gameOver);
        int milliseconds = 500;
        Thread.Sleep(milliseconds);
        soundFXManager.stopSFX();
    }

    public void gamePaused()
    {
        gamePauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void gameResume()
    {
        gamePauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    public void returnToTitle()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1.0f;
    }
}

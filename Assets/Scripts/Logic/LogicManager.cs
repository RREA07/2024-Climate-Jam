using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gamePauseMenu;
    private SoundFXManager soundFXManager;
    public GameObject player;

    void Start()
    {
        soundFXManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundFXManager>();
    }

    //Loads game over
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        Cursor.visible = true;
        //soundFXManager.playSFX(soundFXManager.gameOver);
        Time.timeScale = 0f;
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

    public void reloadCheck()
    {
        gameOverScreen.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        player.GetComponent<Player>().reloadPlayerStats();
    }

    public void returnToTitle()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1.0f;
    }

}

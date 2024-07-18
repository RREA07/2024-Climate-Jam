using System.Collections;
using UnityEngine;

public class HitPause : MonoBehaviour
{
    private float speed;
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            if (Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime * speed;
            }
            else
            {
                Time.timeScale = 1f;
                isPaused = false;
            }
        }
    }

    public void stopTime(float time, int restoreSpeed, float delay)
    {
        speed = restoreSpeed;

        if (delay > 0)
        {
            StopCoroutine(startTimeAgain(delay));
            StartCoroutine(startTimeAgain(delay));
        }
        else
        {
            isPaused = true;
        }
        Time.timeScale = time;
    }

    IEnumerator startTimeAgain(float amt)
    {
        isPaused = true;
        yield return new WaitForSecondsRealtime(amt);
    }
}

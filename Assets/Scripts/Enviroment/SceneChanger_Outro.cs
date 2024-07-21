using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger_Outro : MonoBehaviour
{
    public float changeTime;

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            SceneManager.LoadScene("End Menu");
        }

    }
}

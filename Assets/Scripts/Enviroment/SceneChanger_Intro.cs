using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger_Intro : MonoBehaviour
{
    public float changeTime = 17.1f;

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            SceneManager.LoadScene("Forest_lvl_Latest");
        }

    }
}

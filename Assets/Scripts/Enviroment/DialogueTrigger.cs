using UnityEngine;

public class Enviroment : MonoBehaviour
{
    [Header("Dialogue Contents")]
    public string npcName;
    public string[] npcSentences;
    private int encounter;

    private void Start()
    {
        encounter = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered dialgue trigger zone");
        //triggers cutscene
        if (collision.CompareTag("Player"))
        {
            if (encounter == 0)
            {

                encounter++;
            }
            else
            {

            }
        }
    }
}

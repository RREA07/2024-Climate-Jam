using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public Dialogues dialogues;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered dialgue trigger zone");
        //triggers dialogue
    }
}

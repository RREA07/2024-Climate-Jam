using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string npcName;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string npcSentences;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager manager;
    public Animator ani;
    public Player player;
    //public bool passedFirst = false;

    private void Update()
    {

        if (manager.encounters == 1)
        {
            if (manager.numDialogue == 12)
            {
                Debug.Log("reached leap 1");
                ani.SetTrigger("Leap");
                player.canAttack = true;
                player.dJump = true;
            }
        }
        else if (manager.encounters == 2)
        {
            if (manager.numDialogue == 14)
            {
                ani.SetTrigger("Leap");
            }
        }

        if (manager.isB4Boss)
        {
            ani.SetBool("B4Boss", true);
        }
    }

    public void triggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.canMove = false;
            Cursor.visible = true;
            triggerDialogue();
            //passedFirst = true;
        }
    }
}

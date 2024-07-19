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
    public Animator ani;
    public int encounters = 0;

    private void Update()
    {
        if (DialogueManager.instance.dialogueIsActive)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
        }
    }
}

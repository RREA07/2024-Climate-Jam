using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DialogueManager : MonoBehaviour
{
    public SoundFXManager SoundFXManager;
    public static DialogueManager instance;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI textBox;
    private Queue<DialogueLine> lines;
    public Player player;
    [SerializeField] public bool dialogueIsActive = false;
    public float typeSpeed = 5f;
    public GameObject dialoguePanel;
    public Animator aniWolf;
    public Animator aniChat;
    public int numDialogue = 0;
    public bool canDestroy;
    // Start is called before the first frame update
    void Start()
    {
        canDestroy = false;
        if (instance == null)
        {
            instance = this;
        }

        if (lines == null)
        {
            lines = new Queue<DialogueLine>();
            lines.Clear();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        canDestroy = false;
        numDialogue = 0;
        dialogueIsActive = true;
        aniChat.Play("show");
        lines.Clear();
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        Debug.Log("Current Line: " + lines.Count);
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        numDialogue++;
        DialogueLine currentLine = lines.Dequeue();
        characterName.text = currentLine.character.npcName;
        SoundFXManager.playSFX(SoundFXManager.speech);
        StopAllCoroutines();
        StartCoroutine(typeSentences(currentLine));
    }

    IEnumerator typeSentences(DialogueLine text)
    {
        textBox.text = "";
        foreach (char letter in text.npcSentences.ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    void EndDialogue()
    {
        dialogueIsActive = false;
        Cursor.visible = false;
        aniChat.Play("hide");
        player.canMove = true;
        numDialogue = 0;
        canDestroy = true;
    }
}

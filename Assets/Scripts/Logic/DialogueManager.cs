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
    public float typeSpeed = 0.2f;
    public GameObject dialoguePanel;
    public Animator aniWolf;
    public Animator aniChat;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueIsActive = true;
        dialoguePanel.SetActive(true);
        lines.Clear();
        aniChat.Play("show");
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

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
    }
}

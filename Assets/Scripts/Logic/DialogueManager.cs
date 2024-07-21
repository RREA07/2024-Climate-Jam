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
    public float typeSpeed = 0.03f;
    public GameObject dialoguePanel;
    public Animator aniWolf;
    public Animator aniChat;
    public int numDialogue = 0;
    public bool canDestroy;
    public GameObject encounter1;
    public GameObject encounter2;
    public GameObject encounter3;
    public int encounters = 0;
    public bool isB4Boss = false;
    public LogicManager LogicManager;
    [SerializeField] public float readingTime = 7f;
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

        encounter2.SetActive(false);
        encounter3.SetActive(false);
    }
    private void Update()
    {
        if (readingTime > 0)
        {
            readingTime -= Time.deltaTime;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        encounters++;
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
            lines.Clear();
            EndDialogue();
            return;
        }
        else if (readingTime <= 0)
        {
            readingTime = 3f;
            numDialogue++;
            DialogueLine currentLine = lines.Dequeue();
            characterName.text = currentLine.character.npcName;
            SoundFXManager.playSFX(SoundFXManager.speech);
            StopAllCoroutines();
            StartCoroutine(typeSentences(currentLine));
        }
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
        destropNpc(encounters);

        if (encounters == 3)
        {
            LogicManager.goToEndScene();
        }
    }

    public void destropNpc(int encounter)
    {
        if (!dialogueIsActive && canDestroy)
        {
            player.hasMask = true;
            if (encounter == 1)
            {
                Destroy(encounter1);
                encounter2.SetActive(true);
            }
            else if (encounter == 2)
            {
                Destroy(encounter2);
                encounter3.SetActive(true);
                isB4Boss = true;
            }
        }
    }
}

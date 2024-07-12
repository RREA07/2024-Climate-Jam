using TMPro;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public Dialogues dialogues;
    [Header("Dialogue Contents")]
    public string[] npcSentences;
    private bool passedSceneOne;
    private bool passedSceneTwo;
    private int encounter;
    private TMP_Text speakerName;
    private TMP_Text speakerText;

    private void Start()
    {
        encounter = 0;
        passedSceneOne = false;
        passedSceneTwo = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered dialgue trigger zone");
        //triggers dialogue
        if (collision.CompareTag("Player"))
        {
            if (encounter == 0)
            {
                passedSceneOne = true;
                encounter++;
            }
            else
            {
                passedSceneTwo = true;
            }
        }
    }

    string currentPanelName;
    bool firstEncounter = true;
    int index = 0;
    private void talk()
    {
        if (firstEncounter)
        {
            if (npcSentences[index].Length == 1 && npcSentences[index] == "P")
            {
                currentPanelName = "playerPanel";
                index++;
            }
            if (npcSentences[index].Length == 1 && npcSentences[index] == "N")
            {
                currentPanelName = "npcPanel";
                index++;
            }
            speakerText.text = npcSentences[index];
            switchPanel(currentPanelName);
            index++;
            firstEncounter = false;
        }
    }

    string currentOpenPanel;
    private void switchPanel(string panelName)
    {
        DialogueManager.GetDialogueManager().closePanel(currentOpenPanel);
        GameObject go = DialogueManager.GetDialogueManager().openPanel(panelName);
        foreach (Transform item in go.transform)
        {
            currentOpenPanel = panelName;
        }
    }
}

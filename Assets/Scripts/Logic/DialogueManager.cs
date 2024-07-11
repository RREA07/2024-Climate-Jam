using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager dm;

    private void Awake()
    {
        if (dm == null)
        {
            dm = this;
        }
    }

    public static DialogueManager GetDialogueManager()
    {
        return dm;
    }

    public List<Transform> panels = new List<Transform>();

    private void Start()
    {
        foreach (Transform item in panels)
        {
            item.gameObject.SetActive(false);
        }
    }

    public GameObject openPanel(string panelName)
    {
        foreach (Transform item in panels)
        {
            if (item.name == panelName)
            {
                GameObject activeDialogue = item.gameObject;
                activeDialogue.SetActive(true);
                return activeDialogue;
            }
        }
        Debug.Log("Couldn't find dialogue panel.");
        return null;
    }

    public void closePanel(string panelName)
    {
        foreach (Transform item in panels)
        {
            if (item.name == panelName)
            {
                GameObject deactiveDialogue = item.gameObject;
                deactiveDialogue.SetActive(false);
                return;
            }
        }
        Debug.Log("Couldn't find dialogue panel.");
    }
}

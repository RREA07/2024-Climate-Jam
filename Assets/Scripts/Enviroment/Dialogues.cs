using UnityEngine;

[System.Serializable]
public class Dialogues : MonoBehaviour
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}

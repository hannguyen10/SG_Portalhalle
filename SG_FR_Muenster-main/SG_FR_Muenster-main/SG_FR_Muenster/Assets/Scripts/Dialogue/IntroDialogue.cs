using UnityEngine;

public class IntroDialogueStarter : MonoBehaviour
{
    public Dialogue dialogueManager;
    [TextArea(3, 5)]
    public string[] introLines;

    void Start()
    {
        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue(introLines);
    }
}
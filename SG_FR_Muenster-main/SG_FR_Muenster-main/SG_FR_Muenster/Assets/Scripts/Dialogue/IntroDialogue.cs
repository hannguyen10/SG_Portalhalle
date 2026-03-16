using UnityEngine;

public class IntroDialogueStarter : MonoBehaviour
{
    public Dialogue dialogueManager;
    [TextArea(3, 5)]
    public string[] introLines;

   void Start()
    {
        DialogueLine[] dialogueLines = new DialogueLine[introLines.Length];

        for (int i = 0; i < introLines.Length; i++)
        {
            dialogueLines[i] = new DialogueLine();
            dialogueLines[i].text = introLines[i];
        }

        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue(dialogueLines);
    }
}
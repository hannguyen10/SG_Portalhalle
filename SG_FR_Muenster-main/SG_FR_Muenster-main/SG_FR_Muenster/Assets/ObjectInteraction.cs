using UnityEngine;

public class ObjectInteraction : MonoBehaviour, IInteractable
{


    [Header("Dialog")]
    public Dialogue dialogueManager;
    public DialogueLine[] dialogueLines;

    [Header("Quiz")]
    public QuizData quizData;
    public QuizManager quizManager;

    [Header("Hint")]
    public InteractionHintUI hintUI;
    public string hintText = "Klicke, um mehr zu erfahren";

    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();

        if (outline != null)
        {
            outline.enabled = false;
        }
        else
        {
            Debug.LogWarning("Kein Outline Component auf " + gameObject.name);
        }
    }

    public void OnHoverEnter()
    {
       
        if (Dialogue.IsDialogueActive || QuizManager.IsQuizActive)
            return;

        outline.enabled = true;
        hintUI?.Show(hintText);
    }

    public void OnHoverExit()
    {
        outline.enabled = false;
        hintUI?.Hide();
    }

    public void Interact()
    {
        if (Dialogue.IsDialogueActive) return;


        if (hintUI != null)
            hintUI.Hide();

        outline.enabled = false;

        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue(dialogueLines, false);

        if (quizData != null)
        {
            dialogueManager.SetQuizAfterDialogue(quizData);
        }
    }
}
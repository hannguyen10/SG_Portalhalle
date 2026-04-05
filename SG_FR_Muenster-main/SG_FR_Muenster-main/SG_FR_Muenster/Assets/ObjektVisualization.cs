using UnityEngine;

public class ObjektVisualization : MonoBehaviour, IInteractable
{
    private Renderer[] rends;
    private Material[] mats;

    private Color baseEmission = Color.black;
    private Color glowEmission = Color.yellow * 2f;

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
        Debug.Log("Hover über Objekt: " + gameObject.name);
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

        Debug.Log("Interaktion gestartet");

        if (hintUI != null)
            hintUI.Hide();

        outline.enabled = false;

        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue(dialogueLines);

        if (quizData != null)
        {
            dialogueManager.SetQuizAfterDialogue(quizData);
        }
    }
}
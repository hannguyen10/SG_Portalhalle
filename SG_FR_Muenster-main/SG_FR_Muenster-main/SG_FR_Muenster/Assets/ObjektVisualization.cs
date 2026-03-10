using UnityEngine;

public class ObjektVisualization : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Material mat;

    private Color baseEmission = Color.black;
    private Color glowEmission = Color.yellow * 2f; // Intensität!

    [Header("Dialog")]
    public Dialogue dialogueManager;
    [TextArea(3, 5)]
    public string[] dialogueLines;

    [Header("Quiz")]
    public QuizData quizData;
    public QuizManager quizManager;

    [Header("Hint")]
    public InteractionHintUI hintUI;
    public string hintText = "Klicke, um mehr zu erfahren";


    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
        mat.EnableKeyword("_EMISSION");
    }

    public void OnHoverEnter()
    {
        if (Dialogue.IsDialogueActive || QuizManager.IsQuizActive)
        return;

        mat.SetColor("_EmissionColor", glowEmission);
        hintUI?.Show(hintText);
    }

    public void OnHoverExit()
    {
        mat.SetColor("_EmissionColor", baseEmission);
        hintUI?.Hide();
    }

    public void Interact()
    {

        if (Dialogue.IsDialogueActive) return;

        Debug.Log("Interaktion gestartet");

        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue(dialogueLines);

        if (quizData != null)
        {
            Debug.Log("Quiz an Dialog angehängt");
            dialogueManager.SetQuizAfterDialogue(quizData);
        }
        else
        {
            Debug.LogWarning("Dieses Objekt hat kein QuizData!");
        }
    }
}


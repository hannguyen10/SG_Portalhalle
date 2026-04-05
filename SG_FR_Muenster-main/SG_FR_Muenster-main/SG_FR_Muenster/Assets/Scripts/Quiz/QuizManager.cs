using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup canvasGroup;
    public Dialogue dialogueManager;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI feedbackText;
    public GameObject darkBackground;

    [Header("Player")]
    public MouseMovement mouseMovement;

    public static bool IsQuizActive = false;
    private QuizData currentQuiz;

    void Start()
    {
        // Startzustand: unsichtbar
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void StartQuizDelayed(QuizData quiz)
    {
        StartCoroutine(StartQuizNextFrame(quiz));
    }

    IEnumerator StartQuizNextFrame(QuizData quiz)
    {
        yield return null; // 1 Frame warten
        StartQuiz(quiz);
    }

    public void StartQuiz(QuizData quiz)
    {
        Debug.Log("Quiz gestartet");

        IsQuizActive = true;
        currentQuiz = quiz;

        if (darkBackground != null)
            darkBackground.SetActive(true);

        // Kamera & Maus
        mouseMovement.lookEnabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // UI sichtbar
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // Inhalte setzen
        questionText.text = quiz.question;
        feedbackText.text = "";

        // Buttons konfigurieren
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].interactable = true;
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text =
                quiz.answers[i];

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => Answer(index));
        }
    }

    void Answer(int index)
    {
        foreach (Button b in answerButtons)
            b.interactable = false;

        string[] explanationLines =
            (index == currentQuiz.correctAnswerIndex)
            ? currentQuiz.correctExplanationLines
            : currentQuiz.wrongExplanationLines;

        CloseQuiz();

        DialogueLine[] dialogueLines = new DialogueLine[explanationLines.Length];

        Sprite resultSprite = (index == currentQuiz.correctAnswerIndex)
            ? currentQuiz.correctSprite
            : currentQuiz.wrongSprite;

        for (int i = 0; i < explanationLines.Length; i++)
        {
            dialogueLines[i] = new DialogueLine();
            dialogueLines[i].text = explanationLines[i];
            dialogueLines[i].portrait = resultSprite;
        }
        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue(dialogueLines);
    }


    void CloseQuiz()
    {
        IsQuizActive = false;

        // UI ausblenden
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        if (darkBackground != null)
            darkBackground.SetActive(false);

        // Gameplay zurück
        mouseMovement.lookEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

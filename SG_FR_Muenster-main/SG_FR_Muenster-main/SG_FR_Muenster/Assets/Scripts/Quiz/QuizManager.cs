using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI feedbackText;

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
        Debug.Log("Antwort geklickt: " + index);

        // Buttons deaktivieren
        foreach (Button b in answerButtons)
            b.interactable = false;

        if (index == currentQuiz.correctAnswerIndex)
        {
            feedbackText.text = currentQuiz.correctExplanation;
        }
        else
        {
             feedbackText.text = currentQuiz.wrongExplanation;
        }

        Invoke(nameof(CloseQuiz), 3f);
    }

    void CloseQuiz()
    {
        IsQuizActive = false;

        // UI ausblenden
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Gameplay zurück
        mouseMovement.lookEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

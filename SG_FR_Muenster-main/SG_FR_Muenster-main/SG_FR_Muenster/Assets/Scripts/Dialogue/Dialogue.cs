using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public UnityEngine.UI.Image dialogueImage;
    public UnityEngine.UI.Image portraitImage;
    public Sprite defaultPortrait;
    public MouseMovement mouseMovement;
    public TextMeshProUGUI textComponent;
    private DialogueLine[] lines;
    public GameObject darkBackground;

    private int index;
    private bool dialogueActive = false;
    public static bool IsDialogueActive = false;
    private bool isEndDialogue = false;

    public System.Action OnDialogueFinished;

    public QuizManager quizManager;
    private QuizData pendingQuiz;


    void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void SetQuizAfterDialogue(QuizData quiz)
    {
        pendingQuiz = quiz;
    }

    public void StartDialogue(DialogueLine[] dialogueLines, bool endDialogue = false)
    {
        if (IsDialogueActive) return;
        pendingQuiz = null;

        IsDialogueActive = true;
        dialogueActive = true;

        if (portraitImage != null && defaultPortrait != null)
        {
            portraitImage.sprite = defaultPortrait;
        }

        if (darkBackground != null)
            darkBackground.SetActive(true);

        lines = dialogueLines;
        index = 0;

        isEndDialogue = endDialogue;

        mouseMovement.lookEnabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ShowLine(); // ← direkt anzeigen
    }

    void ShowLine()
    {
        textComponent.text = lines[index].text;

        // Großes Bild
        if (lines[index].image != null)
        {
            dialogueImage.gameObject.SetActive(true);
            dialogueImage.sprite = lines[index].image;
        }
        else
        {
            dialogueImage.gameObject.SetActive(false);
        }

        // Portrait
        if (lines[index].portrait != null)
        {
            portraitImage.sprite = lines[index].portrait;
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            ShowLine();
            return;
        }

        // === Dialog endet ===
        dialogueActive = false;
        IsDialogueActive = false;

        gameObject.SetActive(false);

        if (pendingQuiz == null)
        {
            if (darkBackground != null)
                darkBackground.SetActive(false);
        }

        if (isEndDialogue)
        {
            OnDialogueFinished?.Invoke();
        }

        if (pendingQuiz != null && quizManager != null)
        {
            quizManager.StartQuizDelayed(pendingQuiz);
            pendingQuiz = null;
            return;
        }

        if (mouseMovement != null)
            mouseMovement.lookEnabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator StartQuizNextFrame(QuizData quiz)
    {
        yield return null; // wartet 1 Frame
        quizManager.StartQuiz(quiz);
    }

    void Awake()
    {
        Debug.Log("DialoguePanel Awake, active = " + gameObject.activeSelf);
    }
}
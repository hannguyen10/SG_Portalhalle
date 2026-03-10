using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public MouseMovement mouseMovement;
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    private string[] lines;

    private int index;
    private bool dialogueActive = false;
    public static bool IsDialogueActive = false;

    public System.Action OnDialogueFinished;

    public QuizManager quizManager;
    private QuizData pendingQuiz;


    void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void SetQuizAfterDialogue(QuizData quiz)
    {
        pendingQuiz = quiz;
    }

    public void StartDialogue(string[] dialogueLines)
    {
        if (IsDialogueActive) return;
        pendingQuiz = null;

        IsDialogueActive = true;
        dialogueActive = true;

        lines = dialogueLines;
        index = 0;

        mouseMovement.lookEnabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        textComponent.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        Debug.Log("Dialogue [" + gameObject.name + "] QuizManager = " + quizManager);
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            return;
        }

        // === Dialog endet ===
        dialogueActive = false;
        IsDialogueActive = false;

        gameObject.SetActive(false);

        // FALL 1: Quiz folgt
        if (pendingQuiz != null && quizManager != null)
        {
            Debug.Log("Starte Quiz nach Dialog");
            quizManager.StartQuizDelayed(pendingQuiz);
            pendingQuiz = null;
            return;
        }

        // FALL 2: Kein Quiz → zurück ins Gameplay
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
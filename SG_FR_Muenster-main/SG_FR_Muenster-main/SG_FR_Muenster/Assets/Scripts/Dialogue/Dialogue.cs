using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public UnityEngine.UI.Image dialogueImage;
    public MouseMovement mouseMovement;
    public TextMeshProUGUI textComponent;
    public UnityEngine.UI.Image portraitImage;
    public float textSpeed;
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
            if (textComponent.text == lines[index].text)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index].text;
            }
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

        if (darkBackground != null)
            darkBackground.SetActive(true);

        lines = dialogueLines;
        index = 0;

        isEndDialogue = endDialogue;

        mouseMovement.lookEnabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        textComponent.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeLine());
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

        // Portrait (Gargulus)
        if (lines[index].portrait != null)
        {
            portraitImage.sprite = lines[index].portrait;
        }
    }

    IEnumerator TypeLine()
    {
        ShowLine();

        textComponent.text = "";
        foreach (char c in lines[index].text.ToCharArray())
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
            ShowLine();
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            return;
        }

        // === Dialog endet ===
        dialogueActive = false;
        IsDialogueActive = false;

        gameObject.SetActive(false);

        if (darkBackground != null)
            darkBackground.SetActive(false);

        if (isEndDialogue)
        {
            if (OnDialogueFinished != null)
            {
                OnDialogueFinished.Invoke();
            }
        }
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
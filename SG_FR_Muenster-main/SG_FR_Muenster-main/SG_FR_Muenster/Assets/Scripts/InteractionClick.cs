using UnityEngine;
using TMPro;

public class InteractionHintUI : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    void Awake()
    {
        Hide();
    }

    public void Show(string text)
    {
        if (Dialogue.IsDialogueActive || QuizManager.IsQuizActive)
        return;
        Debug.Log("Hint SHOW");
        hintText.text = text;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("Hint HIDE");
        gameObject.SetActive(false);
    }
}

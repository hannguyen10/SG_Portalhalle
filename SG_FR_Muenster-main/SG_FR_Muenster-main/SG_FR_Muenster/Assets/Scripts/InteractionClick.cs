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
        
        hintText.text = text;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

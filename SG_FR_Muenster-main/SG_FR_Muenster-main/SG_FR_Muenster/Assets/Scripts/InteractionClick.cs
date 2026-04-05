using UnityEngine;
using TMPro;

public class InteractionHintUI : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    public void Show(string text)
    {
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

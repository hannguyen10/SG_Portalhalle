using UnityEngine;
using TMPro;

public class InteractionHintUI : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    public void Show(string text)
    {
        hintText.text = text;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

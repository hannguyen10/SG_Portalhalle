using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsAfterDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public string creditsSceneName = "Credit Scene";

    void Start()
    {
        dialogue.OnDialogueFinished += LoadCredits;
    }

    void LoadCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }
}
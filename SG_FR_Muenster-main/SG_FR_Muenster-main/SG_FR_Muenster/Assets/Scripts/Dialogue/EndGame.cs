using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public Dialogue dialogueSystem;
    public DialogueLine[] endDialogue;
    public GameObject endConfirmUI;

    public MouseMovement mouseMovement;
    public PlayerMovement playerMovement;

    private bool playerLooking = false;

    void Update()
    {
        if (playerLooking && Input.GetMouseButtonDown(0))
        {
            if (!Dialogue.IsDialogueActive)
            {
                OpenEndPanel();
            }
        }
    }

    public void OpenEndPanel()
    {
        endConfirmUI.SetActive(true);

        if (mouseMovement != null)
            mouseMovement.lookEnabled = false;

        if (playerMovement != null)
            playerMovement.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayerIsLookingAtExit(bool looking)
    {
        playerLooking = looking;
    }

    public void EndGameYes()
    {
        endConfirmUI.SetActive(false);

        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.StartDialogue(endDialogue, true);
    }

    public void EndGameNo()
    {
        endConfirmUI.SetActive(false);

        if (mouseMovement != null)
            mouseMovement.lookEnabled = true;

        if (playerMovement != null)
            playerMovement.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
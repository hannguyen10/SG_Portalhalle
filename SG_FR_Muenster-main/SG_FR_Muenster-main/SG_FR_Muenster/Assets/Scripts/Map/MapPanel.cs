using UnityEngine;

public class MapPanel : MonoBehaviour
{
    public GameObject mapPanel;
    public MouseMovement mouseMovement;
    public PlayerMovement playerMovement;

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleMap();
        }
    }

    void ToggleMap()
    {
        if (Dialogue.IsDialogueActive || QuizManager.IsQuizActive)
            return;

        isOpen = !isOpen;

        mapPanel.SetActive(isOpen);

        if (isOpen)
        {
            // Spieler stoppen
            mouseMovement.lookEnabled = false;
            playerMovement.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Spieler wieder bewegen
            mouseMovement.lookEnabled = true;
            playerMovement.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void CloseMap()
    {
        isOpen = false;
        mapPanel.SetActive(false);

        mouseMovement.lookEnabled = true;
        playerMovement.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
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
            //stop player
            mouseMovement.lookEnabled = false;
            playerMovement.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            
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
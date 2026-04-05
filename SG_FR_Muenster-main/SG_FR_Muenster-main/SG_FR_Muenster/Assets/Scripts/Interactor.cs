using UnityEngine;

interface IInteractable
{
    void Interact();

    void OnHoverEnter();
    void OnHoverExit();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 10f;

    private IInteractable currentInteractable;

    void Update()
    {
        if (Dialogue.IsDialogueActive || QuizManager.IsQuizActive)
        return;
        
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    currentInteractable?.OnHoverExit();
                    currentInteractable = interactable;
                    currentInteractable.OnHoverEnter();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    currentInteractable.Interact();
                }

                return;
            }
        }

        if (currentInteractable != null)
        {
            currentInteractable.OnHoverExit();
            currentInteractable = null;
        }
    }
}

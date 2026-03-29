using UnityEngine;

public class LookAtExit : MonoBehaviour
{
    public float rayDistance = 10f;
    private EndGameTrigger currentExit;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Debug Ray anzeigen (rot im Scene View)
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Ray trifft: " + hit.collider.name);

            // Holt Script auch wenn Collider auf Child liegt
            EndGameTrigger trigger = hit.collider.GetComponentInParent<EndGameTrigger>();

            if (trigger != null)
            {
                Debug.Log("Exit erkannt!");

                if (currentExit != trigger)
                {
                    currentExit = trigger;
                    currentExit.PlayerIsLookingAtExit(true);
                }

                return;
            }
        }

        // Wenn wir nicht mehr auf Exit schauen
        if (currentExit != null)
        {
            Debug.Log("Nicht mehr auf Exit");
            currentExit.PlayerIsLookingAtExit(false);
            currentExit = null;
        }
    }
}
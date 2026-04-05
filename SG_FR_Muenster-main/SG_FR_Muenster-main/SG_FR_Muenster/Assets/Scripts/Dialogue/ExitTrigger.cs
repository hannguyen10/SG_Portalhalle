using UnityEngine;

public class ExitZoneTrigger : MonoBehaviour
{
    public EndGameTrigger endGameTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endGameTrigger.OpenEndPanel();
        }
    }
}
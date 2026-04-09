using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditPanel : MonoBehaviour{

    public GameObject panel;

    public void OpenPanel()
    {
        Debug.Log("OpenPanel wurde aufgerufen");
        if (panel != null)
        {
            panel.SetActive(true);
            panel.transform.SetAsLastSibling();
        }
    }

    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

   
}

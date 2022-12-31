using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uGUI_Toggle : MonoBehaviour
{
    public bool showWhenPlaying;
    public GameObject objectToToggle;

    private void FixedUpdate()
    {
        if (GameManager.main.isPlaying)
        {
            if (showWhenPlaying)
            {
                objectToToggle.SetActive(true);
            }
            else
            {
                objectToToggle.SetActive(false);
            }
        }
        else
        {
            if (!showWhenPlaying)
            {
                objectToToggle.SetActive(true);
            }
            else
            {
                objectToToggle.SetActive(false);
            }
        }
    }
}

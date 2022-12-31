using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class uGUI_Result : MonoBehaviour
{
    public TMP_Text textItself;

    public string winText = "You won! Thanks for playing.";
    public string loseText = "You lost, try again!";

    private void Awake()
    {
        textItself.enabled = false;
    }
    public void SetWin()
    {
        textItself.enabled = true;
        textItself.text = winText;
    }
    public void SetLose()
    {
        textItself.enabled = true;
        textItself.text = loseText;
    }
}

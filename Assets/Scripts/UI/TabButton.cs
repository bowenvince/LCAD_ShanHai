using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButton : MonoBehaviour
{
    public GameObject page;
    private BetiaryScript betiaryScript;

    private void Awake()
    {
        betiaryScript = GameObject.Find("HUD").GetComponent<BetiaryScript>();
    }

    public void SwitchTab() 
    {
        betiaryScript.SwitchTab(page);
    }
}

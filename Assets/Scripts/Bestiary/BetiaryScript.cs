using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetiaryScript : MonoBehaviour
{
    public GameObject Bestiary;
    public bool isBestiaryOpen = false;
    public GameManager gameManager;

    public GameObject current_tab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        { 
            Bestiary.gameObject.SetActive(!Bestiary.gameObject.activeSelf); 
        }
    }
    public void OpenCloseBestiary()
    {
        //if the bestiary isn't open (in normal gameplay)
        if (isBestiaryOpen == false)
        {
            Bestiary.SetActive(true); //open the bestiary
            gameManager.PauseGame(); //pause the game so player can not continue to move
            isBestiaryOpen = true;
        }
        else //if the bestiary is already open
        {
            Bestiary.SetActive(false); //close the bestiary
            gameManager.UnpauseGame(); //unpause the game so the player may move
            isBestiaryOpen = false;
        }
    }

    public void SwitchTab(GameObject new_tab) 
    {
        if (current_tab) 
        {
            current_tab.SetActive(false);
        }
        new_tab.SetActive(true);
        current_tab = new_tab;
    }
}

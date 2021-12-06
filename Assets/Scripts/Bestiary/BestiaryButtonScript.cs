using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script we'll use to have more complex logic for our bestiary button behavior than we could accomplish
 * with just the OnClick and dragging things around in the Inspector. In order to call it, we call the 
 * method we want (must be public) (in this case, OpenCloseBestiary()) in OnClick.
 * */

public class BestiaryButtonScript : MonoBehaviour
{
    public bool isBestiaryOpen = false; // will need this to decide whether to open or close bestiary
    public GameObject bestiary; // will need to drag into the inspector
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // find the game manager, which we'll need to pause the game
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    //this method will be called by the BestiaryButton's OnClick in the inspector
    public void OpenCloseBestiary()
    {
        //if the bestiary isn't open (in normal gameplay)
        if(isBestiaryOpen == false)
        {
            bestiary.SetActive(true); //open the bestiary
            gameManager.PauseGame(); //pause the game so player can not continue to move
            isBestiaryOpen = true;
        }
        else //if the bestiary is already open
        {
            bestiary.SetActive(false); //close the bestiary
            gameManager.UnpauseGame(); //unpause the game so the player may move
            isBestiaryOpen = false; 
        }
    }
}

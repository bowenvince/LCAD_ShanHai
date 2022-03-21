using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Use this script for a lot of general game functions. Right now it's only pausing the game, but there will potentially be 
 * a lot more
 * */

public class GameManager : Singleton<GameManager>
{
    public bool isPaused;

    public bool Moveable;

    public PlayerMovement playerMovement;

    public GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.Find("HUD");
    }


    //will be called all over the place, right now just for opening the bestiary
    public void PauseGame()
    {
        //an easy peasy way to pause the game is to get set timeScale to zero. this will pause all animations, movement, etc. 
        //the only thing to keep in mind with this is if you want coroutines to still run while the game is paused 
        //(i can't remember why but this was a problem in my game), you want to make sure you use WaitforSecondsRealTime, not WaitforSeconds
        Time.timeScale = 0;
        isPaused = true;
        Debug.Log("game paused");
    }

    public void UnpauseGame()
    {
        //reset timescale back to normal time
        Time.timeScale = 1;
        isPaused = false;
    }

    public void EnableMove() 
    {
        if (playerMovement != null)
            playerMovement.enabled = true;
    }

    public void DisableMove()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;
    }

    public void QuitGame() 
    {
        Application.Quit();
        Debug.Log("game quit");
    }

    public void EnableHUD(bool state) 
    {
        //just disable all 3 buttons
        HUD.transform.GetChild(0).gameObject.SetActive(state);
        HUD.transform.GetChild(1).gameObject.SetActive(state);
        HUD.transform.GetChild(2).gameObject.SetActive(state);
    }

}

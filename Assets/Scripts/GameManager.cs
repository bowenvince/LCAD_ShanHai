using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Use this script for a lot of general game functions. Right now it's only pausing the game, but there will potentially be 
 * a lot more
 * */

public class GameManager : MonoBehaviour
{
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

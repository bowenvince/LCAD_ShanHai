using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    public float speed; //movement speed
    float xScale;//xscale, stored for ease of flipping

    private Animator player_animator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        xScale = transform.localScale.x; //save the base scale, in case you mess around with it
        player_animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Move(); //our move funciton uses movePosition, which should be called in FixedUpdate
    }

    private void Move()
    {
        float xInput = Input.GetAxis("Horizontal");//get the input from the Input manager. Will be a number between -1 and 1
        Vector3 movement = new Vector2(xInput, 0); //put that movement into a vector, with zero for the y as there is no vertical movement
        myRigidBody.MovePosition(transform.position + (movement * speed)); //MovePosition wants a vector to move TO, so it has to be movement + current position

        //technically, this could all be one line, but it's easier to read broken up:
        //myRigidBody.MovePosition(new Vector2(Input.GetAxis("Horizontal"), 0) * speed * Time.deltaTime);

        //FLIP SPRITE based on x movement:
        //this will also work on your animations, so you don't have to save separate moveRight moveLeft anims 
        //...unless you want them to be different.
        if (xInput > 0) //if moving in the positive x direction (greater than zero) (right)
        {
            //set the local scale to the standard x scale, and leave y and z the same
            transform.localScale = new Vector3(Mathf.Abs(xScale), transform.localScale.y, transform.localScale.z);
            player_animator.SetBool("IsWalking", true);
        }
        else if (xInput < 0) //else if moving in the negative x direction (less than zero) (left)
        {
            //set the local scale to the opposite on the x, flipping it. leave y and z alone.
            transform.localScale = new Vector3(-1 * Mathf.Abs(xScale), transform.localScale.y, transform.localScale.z);
            player_animator.SetBool("IsWalking", true);
        }
        else 
        {
            player_animator.SetBool("IsWalking", false);
        }
    }
}

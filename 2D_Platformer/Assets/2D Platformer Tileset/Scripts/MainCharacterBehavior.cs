using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehavior : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    public float dashSpeed = 50f;

    Rigidbody2D myRigidbody;
    BoxCollider2D myFeetCollider;
    CapsuleCollider2D myBodyCollider;
    Animator myAnimator;

    bool dash;
    float dashCooldown = 2;
    float nextDash = 0;
    public float fallingThreshold = -0.1f;
    public float jumpingThreshold = .1f;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = .5f;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dash)Move();
        FlipSprite();
        Jump();
        Dash();
    }

    //Movement
    private void Move()//move player
    {
        float control = Input.GetAxis("Horizontal");//get input manager axis

        Vector2 playerPos = new Vector2(control * moveSpeed, myRigidbody.velocity.y);//creates new Vector2 with x=control * moveSpeed
        myRigidbody.velocity = playerPos;//every frame velocity gets updated

        bool isPlayerRunning = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;//Abs is always positive, Epsilon smallest float

        myAnimator.SetBool("IsRunning", isPlayerRunning);//sets Bool of Animator true, if player has any movement
    }

    private void FlipSprite()//flips sprite
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;//Abs is always positive, Epsilon smallest float

        if (hasHorizontalSpeed)
        {    
            //flips scale of Player
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1);//Mathf.Sign return value is 1 when f is positive or zero, -1 when f is negative
        }
    }
    //Jump & Fall
    private bool CheckIfJumping()//Checks if player is jumping, positiv on y-axis
    {
        if (myRigidbody.velocity.y > jumpingThreshold)//fi player falling return true
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Jump()//jumping
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))//if player is touching Foreground Layer
        {
            if(Input.GetButtonDown("Jump"))//Get Jump Button
            {
                //myAnimator.SetTrigger("TakeOf");//set Trigger of Animator
                //Vector2 jumpVelocity = new Vector2(0f,jumpSpeed);//new Vector2(0, jumspeed)
                //myRigidbody.velocity += jumpVelocity;//current velocity get added jumpVelocity   
                myAnimator.SetBool("IsJumping", true);//set Bool of Animator
                myRigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);   
            }
        }
        if(!CheckIfJumping())
        {
            Falling();
            myAnimator.SetBool("IsJumping", false);//set Bool of Animator
        }
    }

    private bool CheckIfFalling()//Checks if player is falling, negativ on y-axis
    {
        if(myRigidbody.velocity.y < fallingThreshold)//fi player falling return true
        {
            return true;
        }else
        {
            return false;

        }
    }
    private void Falling()
    {
        if (CheckIfFalling()) Debug.Log("Falling");
        myAnimator.SetBool("IsFalling", CheckIfFalling());
    }

    //Dash
    private void Dash()//dashing
    {
        float xspeed = myRigidbody.transform.localScale.x;//which direction player facing
        int facingSide = -1;

        if(xspeed == 1)//if facing right
        {
            facingSide = 1;
        }

        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))//if player is touching Foreground Layer
        {
            
            if(Input.GetButtonDown("Dash") && !dash)//Get Dash Button
            {
                StartCoroutine(DashCooldown(facingSide));
            }
            
        }
    }

    private IEnumerator DashCooldown(int facingSide)
    {
        dash = true;
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x,0f); 
        myRigidbody.AddForce(new Vector2(dashSpeed*facingSide,0f),ForceMode2D.Impulse);//adds Force to Rigidbody of type Vector2
        myAnimator.SetBool("IsDashing",dash);
        //myAnimator.SetTrigger("Dash");
        //nextDash = Time.time + dashCooldown;
        yield return new WaitForSeconds(1);
        dash = false;
        myAnimator.SetBool("IsDashing",dash);
    }
}

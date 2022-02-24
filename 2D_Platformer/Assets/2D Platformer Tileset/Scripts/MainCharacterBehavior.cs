using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehavior : MonoBehaviour
{
    [Header("Movement H/V")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;

    [Header("Movement Dash")]
    public float dashSpeed;
    public float dashLength = .2f;
    public float dashCooldown = 1f;
    float activeMoveSpeed;
    float dashCounter;
    float dashCoolCounter;


    //Players components
    GameObject currentPlayer;
    MainCharacterChildren mc;

    Rigidbody2D myRigidbody;
    Collider2D myFeetCollider;
    CapsuleCollider2D myBodyCollider;
    Animator myAnimator;    

    //falling & jumping cache
    float fallingThreshold = -.1f;
    float jumpingThreshold = .1f;

    //
    bool isDashing;
    bool feetIsTouchingForeground;
    bool bodyIsTouchingForeground;
    bool feetIsTouchingPlatform;
    bool bodyIsTouchingPlatform;
    float doubleTapTime;
    KeyCode lastKeyCode;

    // Start is called before the first frame update
    void Start()
    {
        mc = FindObjectOfType<MainCharacterChildren>();
        GetCurrentChild();
        GetComponents(currentPlayer);
        activeMoveSpeed = moveSpeed;
    }

    private void GetComponents(GameObject gameObject)
    {
        if(gameObject != null)
        {
            myRigidbody = gameObject.GetComponent<Rigidbody2D>();
            myAnimator = gameObject.GetComponent<Animator>();
            myFeetCollider = gameObject.GetComponent<BoxCollider2D>();
            myBodyCollider = gameObject.GetComponent<CapsuleCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentChild();
        GetComponents(currentPlayer);
        Move();
        FlipSprite();
        Jump();
        DashRoll();
        IsTouching();
        Debug.Log(currentPlayer);
    }

    

    private void GetCurrentChild()//Get children of gameobject(MainCharacter)
    {
        currentPlayer = mc.GetCurrentPlayer();
    }


    //Movement
    private void Move()//move player
    {
        float control = Input.GetAxis("Horizontal");//get input manager axis

        Vector2 playerPos = new Vector2(control * activeMoveSpeed, myRigidbody.velocity.y);//creates new Vector2 with x=control * moveSpeed
        myRigidbody.velocity = playerPos;//every frame velocity gets updated

        bool isPlayerRunning = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;//Abs is always positive, Epsilon smallest float

        myAnimator.SetBool("IsRunning", isPlayerRunning);//sets Bool of Animator true, if player has any movement
        myAnimator.SetBool("SwordRun", isPlayerRunning);//sets Bool of Animator true, if player has any movement
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
        if(feetIsTouchingForeground || feetIsTouchingPlatform)//if player is touching Foreground Layer
        {
            if(Input.GetButtonDown("Jump"))//Get Jump Button
            {
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
        myAnimator.SetBool("IsFalling", CheckIfFalling());
    }
    private void IsTouching()
    {
        feetIsTouchingForeground = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"));
        bodyIsTouchingForeground = myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"));
        feetIsTouchingPlatform = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        bodyIsTouchingPlatform = myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
    }
    private void DashRoll()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                myAnimator.SetTrigger("Dash");
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }




}

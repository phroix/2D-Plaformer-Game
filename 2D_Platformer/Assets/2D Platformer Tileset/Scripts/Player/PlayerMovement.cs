using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    //Parent Component
    Rigidbody2D myRigidbody2D;
    Collider2D myFeetCollider;
    CapsuleCollider2D myBodyCollider;
    SpriteRenderer mySpriteRenderer;

    //Child component
    Animator childAnimator;

    //Weapons
    GameObject currentWeapon;
    GameObject defaultWeapon;
    GameObject swordWeapon;
    GameObject bowWeapon;

    //falling & jumping cache
    float fallingThreshold = -.1f;
    float jumpingThreshold = .1f;

    //fett and body Collider
    bool feetIsTouchingForeground;
    bool bodyIsTouchingForeground;
    bool feetIsTouchingPlatform;
    bool bodyIsTouchingPlatform;

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        GetChildren();
        SetWeaponActive(true, false, false, defaultWeapon);
        //currentWeapon = defaultWeapon;
        GetComponents();
    }
    // Update is called once per frame
    void Update()
    {
        GetCurrentWeapon();
        Move();
        FlipSprite();
        Jump();
        DashRoll();
        IsTouching();
    }

    //Get Children of this GameObject
    private void GetChildren()
    {
        defaultWeapon = gameObject.transform.GetChild(0).gameObject;
        swordWeapon = gameObject.transform.GetChild(1).gameObject;
        bowWeapon = gameObject.transform.GetChild(2).gameObject;
    }

    //Get Components of this GameObject/child
    private void GetComponents()
    {
        myRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        myFeetCollider = gameObject.GetComponent<BoxCollider2D>();
        myBodyCollider = gameObject.GetComponent<CapsuleCollider2D>();
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = null;
        childAnimator = currentWeapon.GetComponent<Animator>();
    }

    //Gets current equipped weapon
    private void GetCurrentWeapon()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))//Check if 1 is pressed
        {
            SetWeaponActive(true, false, false, defaultWeapon);
            //defaultWeapon.SetActive(true);
            //swordWeapon.SetActive(false);
            //bowWeapon.SetActive(false);
            //currentWeapon = defaultWeapon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))//Check if 2 is pressed
        {
            SetWeaponActive(false, true, false, swordWeapon);
            //defaultWeapon.SetActive(false);
            //swordWeapon.SetActive(true);
            //bowWeapon.SetActive(false);
            //currentWeapon = swordWeapon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))//Check if 3 is pressed
        {
            SetWeaponActive(false, false, true, bowWeapon);
            //defaultWeapon.SetActive(false);
            //swordWeapon.SetActive(false);
            //bowWeapon.SetActive(true);
            //currentWeapon = bowWeapon;
        }

        if (childAnimator != null) childAnimator = currentWeapon.GetComponent<Animator>(); //gets component of child Animator
    }

    private void SetWeaponActive(bool b1, bool b2, bool b3, GameObject g)
    {
        defaultWeapon.SetActive(b1);
        swordWeapon.SetActive(b2);
        bowWeapon.SetActive(b3);
        if(g!=null) currentWeapon = g;
    }
    //move player
    private void Move()
    {
        float control = Input.GetAxis("Horizontal");//get input manager axis

        Vector2 playerPos = new Vector2(control * activeMoveSpeed, myRigidbody2D.velocity.y);//creates new Vector2 with x=control * moveSpeed
        myRigidbody2D.velocity = playerPos;//every frame velocity gets updated

        bool isPlayerRunning = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;//Abs is always positive, Epsilon smallest float

        if (childAnimator != null) childAnimator.SetBool("IsRunning", isPlayerRunning);//sets Bool of Animator true, if player has any movement
        //childAnimator.SetBool("SwordRun", isPlayerRunning);//sets Bool of Animator true, if player has any movement
    }

    //flips sprite
    private void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;//Abs is always positive, Epsilon smallest float

        if (hasHorizontalSpeed)
        {
            //flips scale of Player
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1);//Mathf.Sign return value is 1 when f is positive or zero, -1 when f is negative
        }
    }

    //Checks if player is jumping, positiv on y-axis
    private bool CheckIfJumping()
    {
        if (myRigidbody2D.velocity.y > jumpingThreshold)//if player falling return true
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //jumping player
    private void Jump()
    {
        if (feetIsTouchingForeground || feetIsTouchingPlatform)//if player is touching Foreground Layer
        {
            if (Input.GetButtonDown("Jump"))//Get Jump Button
            {
                childAnimator.SetBool("IsJumping", true);//set Bool of Animator
                myRigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
        if (!CheckIfJumping())
        {
            Falling();
            childAnimator.SetBool("IsJumping", false);//set Bool of Animator
        }
    }

    //Checks if player is falling, negativ on y-axis
    private bool CheckIfFalling()
    {
        if (myRigidbody2D.velocity.y < fallingThreshold)//if player falling return true
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //falling player
    private void Falling()
    {
        childAnimator.SetBool("IsFalling", CheckIfFalling());
    }

    //touching ground player
    private void IsTouching()
    {
        feetIsTouchingForeground = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"));
        bodyIsTouchingForeground = myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"));
        feetIsTouchingPlatform = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        bodyIsTouchingPlatform = myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
    }

    //dashroll player
    private void DashRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))//chek if LeftShift is pressed
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                childAnimator.SetTrigger("Dash");
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }
}

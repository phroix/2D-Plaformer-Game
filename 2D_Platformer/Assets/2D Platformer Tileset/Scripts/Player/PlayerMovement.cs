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
    Rigidbody2D parentRigidbody;
    Collider2D parentFeetCollider;
    CapsuleCollider2D parentBodyCollider;
    SpriteRenderer parentSpriteRenderer;

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
        activeMoveSpeed = moveSpeed;
        GetChildren();
        currentWeapon = defaultWeapon;
        GetComponents();
    }

    private void GetChildren()
    {
        defaultWeapon = gameObject.transform.GetChild(0).gameObject;
        swordWeapon = gameObject.transform.GetChild(1).gameObject;
        bowWeapon = gameObject.transform.GetChild(2).gameObject;
    }

    private void GetComponents()
    {
        parentRigidbody = gameObject.GetComponent<Rigidbody2D>();
        parentFeetCollider = gameObject.GetComponent<BoxCollider2D>();
        parentBodyCollider = gameObject.GetComponent<CapsuleCollider2D>();
        parentSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        parentSpriteRenderer.sprite = null;
        childAnimator = currentWeapon.GetComponent<Animator>();
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
    private void GetCurrentWeapon()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1");
            defaultWeapon.SetActive(true);
            swordWeapon.SetActive(false);
            bowWeapon.SetActive(false);
            currentWeapon = defaultWeapon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2");
            defaultWeapon.SetActive(false);
            swordWeapon.SetActive(true);
            bowWeapon.SetActive(false);
            currentWeapon = swordWeapon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3");
            defaultWeapon.SetActive(false);
            swordWeapon.SetActive(false);
            bowWeapon.SetActive(true);
            currentWeapon = bowWeapon;
        }

        if(childAnimator != null) childAnimator = currentWeapon.GetComponent<Animator>();
    }


    private void Move()//move player
    {
        float control = Input.GetAxis("Horizontal");//get input manager axis

        Vector2 playerPos = new Vector2(control * activeMoveSpeed, parentRigidbody.velocity.y);//creates new Vector2 with x=control * moveSpeed
        parentRigidbody.velocity = playerPos;//every frame velocity gets updated

        bool isPlayerRunning = Mathf.Abs(parentRigidbody.velocity.x) > Mathf.Epsilon;//Abs is always positive, Epsilon smallest float

        if (childAnimator != null) childAnimator.SetBool("IsRunning", isPlayerRunning);//sets Bool of Animator true, if player has any movement
        //childAnimator.SetBool("SwordRun", isPlayerRunning);//sets Bool of Animator true, if player has any movement
    }

    private void FlipSprite()//flips sprite
    {
        bool hasHorizontalSpeed = Mathf.Abs(parentRigidbody.velocity.x) > Mathf.Epsilon;//Abs is always positive, Epsilon smallest float

        if (hasHorizontalSpeed)
        {
            //flips scale of Player
            transform.localScale = new Vector2(Mathf.Sign(parentRigidbody.velocity.x), 1);//Mathf.Sign return value is 1 when f is positive or zero, -1 when f is negative
        }
    }


    private bool CheckIfJumping()//Checks if player is jumping, positiv on y-axis
    {
        if (parentRigidbody.velocity.y > jumpingThreshold)//fi player falling return true
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
        if (feetIsTouchingForeground || feetIsTouchingPlatform)//if player is touching Foreground Layer
        {
            if (Input.GetButtonDown("Jump"))//Get Jump Button
            {
                childAnimator.SetBool("IsJumping", true);//set Bool of Animator
                parentRigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
        if (!CheckIfJumping())
        {
            Falling();
            childAnimator.SetBool("IsJumping", false);//set Bool of Animator
        }
    }

    private bool CheckIfFalling()//Checks if player is falling, negativ on y-axis
    {
        if (parentRigidbody.velocity.y < fallingThreshold)//fi player falling return true
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Falling()
    {
        childAnimator.SetBool("IsFalling", CheckIfFalling());
    }
    private void IsTouching()
    {
        feetIsTouchingForeground = parentFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"));
        bodyIsTouchingForeground = parentBodyCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"));
        feetIsTouchingPlatform = parentFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        bodyIsTouchingPlatform = parentBodyCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
    }
    private void DashRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
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

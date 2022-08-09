using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildNPCNormalTutorialSystem : MonoBehaviour
{

    public GameObject [] WayPoints;
    public float activeMoveSpeed = 3f;

    float control = 1f;
    bool moveNPC = true;

    Rigidbody2D myRigidbody2D;
    BoxCollider2D myBoxCollider2D;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWayPoints();
        FlipSprite();
    }

    private void MoveToWayPoints()
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("WayPoints")))
        {
            transform.localScale = new Vector2(-1, 1);
            moveNPC = false;
        }

        if (moveNPC)
        {
            Vector2 playerPos = new Vector2(control * activeMoveSpeed, myRigidbody2D.velocity.y);//creates new Vector2 with x=control * moveSpeed
            myRigidbody2D.velocity = playerPos;//every frame velocity gets updated
           //sets Bool of Animator true, if player has any movement
        }
        myAnimator.SetBool("IsRunning", moveNPC);
    }

    private void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;//Abs is always positive, Epsilon smallest float

        if (hasHorizontalSpeed)
        {
            //flips scale of Player
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1);//Mathf.Sign return value is 1 when f is positive or zero, -1 when f is negative
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
              
    }


}

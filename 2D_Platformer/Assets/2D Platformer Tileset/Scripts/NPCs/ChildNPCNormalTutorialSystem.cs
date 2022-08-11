using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildNPCNormalTutorialSystem : MonoBehaviour
{

    public GameObject [] wayPoints;
    public float activeMoveSpeed = 3f;
    public GameObject [] overlayStages;


    float control = 1f;
    bool moveNPC = false;
    bool playerDetected = false;
    static bool canvasOverlayOpened = false;
    int hitWayPoints = 0;

    Rigidbody2D myRigidbody2D;
    BoxCollider2D myBoxCollider2D;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myRigidbody2D.bodyType = RigidbodyType2D.Static;
        overlayStages[0].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hitWayPoints: " + hitWayPoints);
        Debug.Log("overlayStages: " + overlayStages[hitWayPoints].name);
        MoveToWayPoints();
        FlipSprite();
        OpenCanvasOverlay();
    }
    private void OpenCanvasOverlay()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerDetected)
        {
            canvasOverlayOpened = true;
            moveNPC = true;
            overlayStages[hitWayPoints + 1].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            overlayStages[hitWayPoints].SetActive(false);
        }

        if (hitWayPoints == 4)
        {
            myRigidbody2D.bodyType = RigidbodyType2D.Static;
            moveNPC = false;
        }
        //if (!canvasOverlayOpened)
    }
    private void MoveToWayPoints()
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player"))) return;

        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("WayPoints")))
        {
            transform.localScale = new Vector2(-1, 1);
            //myRigidbody2D.velocity = new Vector2(0, 0);
            myRigidbody2D.bodyType = RigidbodyType2D.Static;
            moveNPC = false;
            Destroy(wayPoints[hitWayPoints]);
            ++hitWayPoints;
        }

        if (moveNPC)
        {
            myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
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

    public void ExitOverlayStage()
    {
        Debug.Log("Press");
        overlayStages[hitWayPoints].SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player") return;
        playerDetected = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "Player") return;
        //canvasOverlayOpened = false;
        playerDetected = false;
    }


}

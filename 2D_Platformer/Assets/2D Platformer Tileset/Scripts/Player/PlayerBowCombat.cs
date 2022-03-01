using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowCombat : MonoBehaviour
{
    public GameObject arrow;
    public float normalAttackDamage = 0f;

    public Transform shotpoint;
    public LayerMask enemyLayers;

    [Header("Normal Attack")]
    public float normalArrowSpeed = 100000f;
    public float yAxis = .5f;

    public float normalAttackRange = 0.5f;
    public float normalAttackRate = 2f;

    float nextAttackTime = 0f;
    float nextMove = 0f;

    [Header("Normal Attack")]
    public float qArrowSpeed = 100000f;

    public float qAttackRange = 0.5f;
    public float qAttackRate = 2f;

    float nextQAttackTime = 0f;

    //public float launchForce;

    //Gameobject Components
    Animator myAnimator;

    //Parent Objects
    GameObject parentObject;
    Rigidbody2D parentRigidbody2D;
    BoxCollider2D parentBoxCollider2D;
    bool localScalel;
    float xspeed;

    Coroutine myCoroutine;



    private void Awake()
    {
        parentObject = gameObject.transform.parent.gameObject;
        parentRigidbody2D = parentObject.GetComponent<Rigidbody2D>();
        parentBoxCollider2D = parentObject.GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerScale();
        NormalAttack();
    }

    private void GetPlayerScale()
    {
        xspeed = parentRigidbody2D.transform.localScale.x;
        if (xspeed == 1)
        {
            localScalel = true;
        }
        else if (xspeed == -1)
        {
            localScalel = false;
        }
    }

    private void NormalAttack()
    {
        if (Time.time <= nextMove)
        {
            parentRigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Make Player stand still
                nextMove = Time.time + (1f / normalAttackRate);

                //Play attack animation
                myAnimator.SetTrigger("NormalAttack");          

                nextAttackTime = Time.time + 1f / normalAttackRate;
            }
            else
            {
                parentRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void InstatiateArrow(float arrowSpeed)
    {
        arrowSpeed = normalArrowSpeed;
        GameObject newArrow = Instantiate(arrow, shotpoint.position, shotpoint.rotation) as GameObject;
        //if(!localScalel) newArrow.transform.localScale *= -1;
        newArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(xspeed * normalArrowSpeed, yAxis);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(shotpoint.position, 0.1f);
    //}
}

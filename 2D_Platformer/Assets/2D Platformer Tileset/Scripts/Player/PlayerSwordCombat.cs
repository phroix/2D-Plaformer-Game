using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordCombat : MonoBehaviour
{
    [Header("Normal Attack")]
    public Transform normalAttackPoint;
    public float normalAttackRange = 0.5f;
    public float normalAttackRate = 2f;
    public float normalAttackDamage = 0f;
    float nextAttackTime = 0f;
    public LayerMask enemyLayers;

    //Parent Objects
    GameObject parentObject;
    Rigidbody2D parentRigidbody2D;

    //Component Gameobjects
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = gameObject.transform.parent.gameObject;
        parentRigidbody2D = parentObject.GetComponent<Rigidbody2D>();

        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        NormalAttack();
    }

    //normal attack player with left mouse klick
    private void NormalAttack()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Make Player stand still
                parentRigidbody2D.bodyType = RigidbodyType2D.Static;
                //Play attack animation
                myAnimator.SetTrigger("NormalAttack");

                //Detect enemies in  range of attack
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(normalAttackPoint.position, normalAttackRange, enemyLayers);

                //Damage enemies
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit " + enemy.name);
                }
                nextAttackTime = Time.time + 1f / normalAttackRate;
            }
            else
            {
                parentRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (normalAttackPoint == null) return;
        Gizmos.DrawWireSphere(normalAttackPoint.position, normalAttackRange);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwordCombat : MonoBehaviour
{
    [Header("Normal Attack")]
    public float normalAttackDamage = 0f;

    public Transform normalAttackPoint;
    public LayerMask enemyLayers;

    public float normalAttackRange = 0.5f;
    public float normalAttackRate = 2f;

    float nextAttackTime = 0f;
    float nextMove = 0f;

    [Header("Q Attack")]
    public float qAttackDamage = 0f;

    public Image qAbilityImage;
    public Transform qAttackPoint;

    public float qAttackRange = 0.5f;
    public float qAttackRate = 2f;
    
    public float cooldownQTime = 3f;
    float nextQAttackTime = 0f;
    
    bool isQCooldown = false;

    [Header("E Attack")]
    public float eAttackDamage = 0f;

    public Image eAbilityImage;
    public Transform eAttackPoint;
    
    public float eAttackRange = 0.5f;
    public float eAttackRate = 2f;
    
    public float cooldownETime = 5f;
    float nextEAttackTime = 0f;

    bool isECooldown = false;



    //Parent Objects
    GameObject parentObject;
    Rigidbody2D parentRigidbody2D;
    BoxCollider2D parentBoxCollider2D;

    //Component Gameobjects
    Animator myAnimator;

    private void Awake()
    {
        parentObject = gameObject.transform.parent.gameObject;
        parentRigidbody2D = parentObject.GetComponent<Rigidbody2D>();
        parentBoxCollider2D = parentObject.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        qAbilityImage.fillAmount = 0;
        eAbilityImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (parentBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            NormalAttack(); 
            QAttack();
            EAttack();
        }
    }

    //normal attack player with left mouse klick
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

    private void QAttack()
    {
        //Make Player stand still while attacking
        if (Time.time <= nextMove)
        {
            parentRigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        if (Time.time >= nextQAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Q) && isQCooldown == false)
            {
                Debug.Log("Q pressed");
                //Cooldown
                nextQAttackTime = Time.time + cooldownQTime;
                isQCooldown = true;
                qAbilityImage.fillAmount = 1;
                nextMove = Time.time + (1f / qAttackRate);
                
                //Play attack animation
                myAnimator.SetTrigger("QAttack");

                //Detect enemies in  range of attack
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(qAttackPoint.position, qAttackRange, enemyLayers);

                //Damage enemies
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit with q" + enemy.name);
                }
                
            }else                
            {
                parentRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        
        if (isQCooldown)//Qooldown image fillamount increase
        {
            qAbilityImage.fillAmount = qAbilityImage.fillAmount - (1 / cooldownQTime * Time.deltaTime);

            if (qAbilityImage.fillAmount == 0)
            {
                qAbilityImage.fillAmount = 0;
                isQCooldown = false;
            }
        }

    }
    private void EAttack()
    {
        Debug.Log("E ATTACK");
        //Make Player stand still while attacking
        if (Time.time <= nextMove)
        {
            Debug.Log("RB Static");
            parentRigidbody2D.bodyType = RigidbodyType2D.Static;
        }
        if (Time.time >= nextEAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E) && isECooldown == false)
            {
                Debug.Log("E pressed");
                //Cooldown
                nextEAttackTime = Time.time + cooldownETime;
                isECooldown = true;
                eAbilityImage.fillAmount = 1;
                nextMove = Time.time + (1f / eAttackRate);

                //Play attack animation
                myAnimator.SetTrigger("EAttack");

                //Detect enemies in  range of attack
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(eAttackPoint.position, eAttackRange, enemyLayers);

                //Damage enemies
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit with e" + enemy.name);
                }

            }
            else
            {
                Debug.Log("RB Dynamic");
                parentRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }

        if (isECooldown)//Qooldown image fillamount increase
        {
            Debug.Log("Increase fillamount");
            eAbilityImage.fillAmount = eAbilityImage.fillAmount - (1 / cooldownETime * Time.deltaTime);

            if (eAbilityImage.fillAmount == 0)
            {
                Debug.Log("Incresing done");
                eAbilityImage.fillAmount = 0;
                isECooldown = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (normalAttackPoint == null ||qAttackPoint == null || eAttackPoint == null) return;
        //Gizmos.DrawWireSphere(normalAttackPoint.position, normalAttackRange);
        //Gizmos.DrawWireSphere(qAttackPoint.position, qAttackRange);
        //Gizmos.DrawWireSphere(eAttackPoint.position, eAttackRange);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwordCombat : MonoBehaviour
{
    [Header("Normal Attack")]
    public Transform normalAttackPoint;
    public float normalAttackRange = 0.5f;
    public float normalAttackRate = 2f;
    public float normalAttackDamage = 0f;
    float nextAttackTime = 0f;
    public LayerMask enemyLayers;

    [Header("Q Attack")]
    public Transform qAttackPoint;
    public float qAttackRange = 0.5f;
    public float qAttackRate = 2f;
    public float qAttackDamage = 0f;
    public float nextQAttackTime;
    public int cooldownQTime = 3;
    public Image qAbilityImage;
    bool isQCooldown = false;

    [Header("E Attack")]
    public Transform eAttackPoint;
    public float eAttackRange = 0.5f;
    public float eAttackRate = 2f;
    public float eAttackDamage = 0f;
    public float nextEAttackTime;
    public int cooldownETime = 5;
    public Image eAbilityImage;
    bool isECooldown = false;



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

        qAbilityImage.fillAmount = 0;
        //eAbilityImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        NormalAttack();
        if(Time.time > nextQAttackTime) QAttack();
        if (Time.time > nextEAttackTime) EAttack();

        if (isQCooldown == false)
        {

            
        }
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

    private void QAttack()
    {
        if (Time.time >= nextAttackTime)
        {
           

            if (Input.GetKeyDown(KeyCode.Q) && isQCooldown == false)
            {
                //Cooldown
                nextQAttackTime = Time.time + cooldownQTime;

                isQCooldown = true;
                qAbilityImage.fillAmount = 1;

                

                //Make Player stand still
                parentRigidbody2D.bodyType = RigidbodyType2D.Static;
                //Play attack animation
                myAnimator.SetTrigger("QAttack");

                //Detect enemies in  range of attack
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(qAttackPoint.position, qAttackRange, enemyLayers);

                //Damage enemies
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit with q" + enemy.name);
                }
                nextAttackTime = Time.time + 1f / qAttackRate;
            }
            else
            {
                parentRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }

            if (isQCooldown)
            {
                Debug.Log("test");
                qAbilityImage.fillAmount -= 1 / cooldownQTime * Time.deltaTime;

                if (qAbilityImage.fillAmount <= 0)
                {
                    qAbilityImage.fillAmount = 0;
                    isQCooldown = false;
                }
            }
        }
        
    }


    private void EAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Make Cooldown
                nextEAttackTime = Time.time + cooldownETime;
                //Make Player stand still
                parentRigidbody2D.bodyType = RigidbodyType2D.Static;
                //Play attack animation
                myAnimator.SetTrigger("EAttack");

                //Detect enemies in  range of attack
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(eAttackPoint.position, eAttackRange, enemyLayers);

                //Damage enemies
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit with e" + enemy.name);
                }
                nextAttackTime = Time.time + 1f / eAttackRate;
            }
            else
            {
                parentRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (normalAttackPoint == null ||qAttackPoint == null || eAttackPoint == null) return;
        //Gizmos.DrawWireSphere(normalAttackPoint.position, normalAttackRange);
        Gizmos.DrawWireSphere(qAttackPoint.position, qAttackRange);
        //Gizmos.DrawWireSphere(eAttackPoint.position, eAttackRange);
    }
}

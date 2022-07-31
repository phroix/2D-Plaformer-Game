using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerBowCombat : MonoBehaviour
{
    public GameObject arrow;
    public Text amountArrowsText;
    public int currentAmountArrows = 0;

    public Transform shotpoint;
    public LayerMask enemyLayers;

    [Header("Normal Attack")]
    public float normalAttackDamage = 0f;
    public float normalArrowSpeed = 100000f;
    public float yAxis = .5f;
    public float normalAttackRange = 0.5f;
    public float normalAttackRate = 2f;
    float nextAttackTime = 0f;
    float nextMove = 0f;

    [Header("Q Attack")]
    public float qAttackDamage = 0f;
    public Image qAbilityImage;
    public float qAttackRange = 0.5f;
    public float qAttackRate = 2f;
    public float cooldownQTime = 5f;
    public int qEnergCost = 30;
    float nextQAttackTime = 0f;
    bool isQCooldown = false;

    [Header("E Attack")]
    public float eAttackDamage = 0f;
    public GameObject firearrow;
    public Image eAbilityImage;
    public float eAttackRange = 0.5f;
    public float eAttackRate = 2f;
    public float cooldownETime = 3f;
    public int eEnergCost = 20;
    float nextEAttackTime = 0f;
    bool isECooldown = false;

    //Gameobject Components
    Animator myAnimator;

    //Parent Objects
    GameObject parentObject;
    Rigidbody2D parentRigidbody2D;
    BoxCollider2D parentBoxCollider2D;
    bool localScalel;
    float xspeed;

    PlayerHealthXpSystem myPlayerHealthXpSystem;
    PotWheelMenuController myPotWheelMenuController;
    ParentNPCSystem myParentNPCSystem;


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
        myPlayerHealthXpSystem = FindObjectOfType<PlayerHealthXpSystem>();
        myPotWheelMenuController = FindObjectOfType<PotWheelMenuController>();
        myParentNPCSystem = FindObjectOfType<ParentNPCSystem>();
        qAbilityImage.fillAmount = 0;
        eAbilityImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerScale();
        NormalAttack();
        QAttack();
        EAttack();
        DisplayArrowAmount();
    }

    private void DisplayArrowAmount()
    {
        amountArrowsText.text = currentAmountArrows.ToString();
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
        if (myParentNPCSystem.GetCanvasOverlayOpened() || myPotWheelMenuController.GetPotWheelSelected()) return;

        if (Time.time <= nextMove)
        {
            parentRigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmountArrows > 0)
            {
                //Make Player stand still
                nextMove = Time.time + (1f / normalAttackRate);

                //Play attack animation
                DecreaseArrows(1);
                myAnimator.SetTrigger("NormalAttack");          

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
        if (myPlayerHealthXpSystem.GetCurrentEnergy() < qEnergCost && !isQCooldown) return;

        if (Time.time <= nextMove)
        {
            parentRigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        if (Time.time >= nextQAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !isQCooldown && !myPotWheelMenuController.GetPotWheelSelected() && currentAmountArrows >= 3)
            {
                //Make Player stand still
                myPlayerHealthXpSystem.DecreaseEnergy(qEnergCost);
                isQCooldown = true;
                qAbilityImage.fillAmount = 1;
                nextMove = Time.time + (1f / qAttackRate);

                //Play attack animation
                DecreaseArrows(3);
                myAnimator.SetTrigger("QAttack");

                nextQAttackTime = Time.time + 1f / qAttackRate;
            }
            else
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
        if (myPlayerHealthXpSystem.GetCurrentEnergy() < eEnergCost && !isECooldown) return;

        if (Time.time <= nextMove)
        {
            parentRigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        if (Time.time >= nextEAttackTime && !isECooldown && !myPotWheelMenuController.GetPotWheelSelected() && currentAmountArrows > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Make Player stand still
                myPlayerHealthXpSystem.DecreaseEnergy(eEnergCost);
                isECooldown = true;
                eAbilityImage.fillAmount = 1;
                nextMove = Time.time + (1f / eAttackRate);

                DecreaseArrows(1);

                //Play attack animation
                myAnimator.SetTrigger("EAttack");

                nextEAttackTime = Time.time + 1f / eAttackRate;
            }
            else
            {
                parentRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }

        if (isECooldown)//Qooldown image fillamount increase
        {
            eAbilityImage.fillAmount = eAbilityImage.fillAmount - (1 / cooldownETime * Time.deltaTime);

            if (eAbilityImage.fillAmount == 0)
            {
                eAbilityImage.fillAmount = 0;
                isECooldown = false;
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

    private void InstatiateFireArrow(float arrowSpeed)
    {
        arrowSpeed = normalArrowSpeed;
        GameObject newArrow = Instantiate(firearrow, shotpoint.position, shotpoint.rotation) as GameObject;
        //if(!localScalel) newArrow.transform.localScale *= -1;
        newArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(xspeed * normalArrowSpeed, yAxis);
    }

    public float GetQCooldown()
    {
        return cooldownQTime;
    }
    public float GetECooldown()
    {
        return cooldownETime;
    }

    public void SetQCooldown(float q)
    {
        cooldownQTime = q;
    }
    public void SetECooldown(float e)
    {
        cooldownETime = e;
    }

    public void IncreaseArrows()
    {
        ++currentAmountArrows;
    }    
    public void DecreaseArrows(int i)
    {
        currentAmountArrows -= i;
    }

    public void IncreaseNormalATK(int i)
    {
        normalAttackDamage += i;
    }

    public void IncreaseQATK(int i)
    {
        qAttackDamage += i;
    }

    public void IncreaseEATK(int i)
    {
        eAttackDamage += i;
    }


}

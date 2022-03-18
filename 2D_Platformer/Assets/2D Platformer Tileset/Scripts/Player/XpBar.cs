using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class XpBar : MonoBehaviour
{
    public Image xpBar;

    public void SetStartXP(float xp)
    {
        xpBar.fillAmount = xp;
        //XpSlider.maxValue = xp;
        //XpSlider.value = xp;
    }

    //if (isQCooldown)//Qooldown image fillamount increase
    //    {
    //        qAbilityImage.fillAmount = qAbilityImage.fillAmount - (1 / cooldownQTime* Time.deltaTime);

    //        if (qAbilityImage.fillAmount == 0)
    //        {
    //            qAbilityImage.fillAmount = 0;
    //            isQCooldown = false;
    //        }
    //    }

    public void SetXP(float xp)
    {
        if(xp != 0)
        {
            xpBar.fillAmount = 1 / xp;
        }
        else
        {
            xpBar.fillAmount = xp;
        }
    }

    public void AddXP(float xp)
    {
        xpBar.fillAmount += (1 / xp) / 4;
    }
}

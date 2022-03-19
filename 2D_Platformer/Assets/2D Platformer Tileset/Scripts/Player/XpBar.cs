using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class XpBar : MonoBehaviour
{
    public Slider xpSlider;

    public void SetMaxXP(int xp) //For one level
    {
        xpSlider.maxValue = xp;
        xpSlider.value = xp;
    }

    public void ResetXP(int xp)
    {
        xpSlider.maxValue = xp;
        xpSlider.value = 0;
    }

    public void SetXP(int xp)
    {
        xpSlider.value = xp;
    }
}

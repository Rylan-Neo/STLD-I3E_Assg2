/*
* Author: Rylan Neo
* Date of creation: 18th June 2024
* Description: Code for the health bar UI
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    // healthbar is a slider with just a fill
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    /// <summary>
    /// the value of health correalates to a value which corresponds to a colour on teh gradient that is set
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(float health)
    {  
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    /// <summary>
    /// Sets max value for the health slider
    /// </summary>
    /// <param name="max"></param>
    public void SetMaxHealth(float max)
    { 
        slider.maxValue = max;
        slider.value = max;
        fill.color = gradient.Evaluate(1f);
    }
}

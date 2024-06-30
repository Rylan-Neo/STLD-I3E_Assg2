/*
* Author: Rylan Neo
* Date of creation: 19th June 2024
* Description: Code for the sprint bar UI
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{

    // sprintbar is a slider with just a fill
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    /// <summary>
    /// the value of sprint correalates to a value which corresponds to a colour on teh gradient that is set
    /// </summary>
    /// <param name="sprint"></param>
    public void SetSprint(float sprint)
    {
        slider.value = sprint;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    /// <summary>
    /// Sets the max sprint for the slider
    /// </summary>
    /// <param name="max"></param>
    public void SetMaxSprint(float max)
    {
        slider.maxValue = max;
        slider.value = max;
        fill.color = gradient.Evaluate(1f);
    }
}

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
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetSprint(float sprint)
    {
        slider.value = sprint;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxSprint(float max)
    {
        slider.maxValue = max;
        slider.value = max;
        fill.color = gradient.Evaluate(1f);
    }
}

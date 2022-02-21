using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxValueOnBar(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetOnlyMaxValueOnBar(float maxValue)
    {
        slider.maxValue = maxValue;
    }
    public void SetValueOnBar(float value)
    {
        slider.value = value;
    }
}

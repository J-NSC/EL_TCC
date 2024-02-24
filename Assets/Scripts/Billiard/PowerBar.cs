using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxPowerBar(int powerBar)
    {
        slider.maxValue = powerBar;
        slider.value = 0;

        fill.color = gradient.Evaluate(1f);
    }
    
    public void SetPowerBar(float powerBar)
    {
        slider.value = powerBar;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void ResetPowerBar()
    {
        slider.value = 0; 
    }
}

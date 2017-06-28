using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleManager : MonoBehaviour
{
    public LButton plusButton, minusButton;
    public LSlider slider;

    void Start()
    {
        plusButton.onClick = delegate { slider.fillAmount += 0.1f; };
        minusButton.onClick = delegate { slider.fillAmount -= 0.1f; };
    }
}
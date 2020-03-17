using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderAwake : MonoBehaviour
{
    public string KeyToGrabFrom = "";
    Slider SliderToUpdate = null;

    private void Awake()
    {
        SliderToUpdate = GetComponent<Slider>();

        if(PlayerPrefs.HasKey(KeyToGrabFrom))
        {
            SliderToUpdate.value = PlayerPrefs.GetFloat(KeyToGrabFrom);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowValueScript : MonoBehaviour
{
    //Transfers the value of each slider to a TExtMeshProUGUI text. 

    TextMeshProUGUI valueText;
    void Start()
    {
        //get the default slider value 
        valueText = GetComponent<TextMeshProUGUI>();
    }

    public void textUpdate(float value)
    {
        //transfer the slidervalue into text. 
        valueText.text = "Value = " + value;
    }
} 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetTemperatur : MonoBehaviour
{
    //Script to transfer the var Temperartur form the Extract Image Script to a text. 

    TextMeshProUGUI Temp_value;

    // Start is called before the first frame update
    void Start()
    {
        //Eventsystem,which listens to changes in aTemperatureEvent
        SliderEventSystem.aTemperatureEvent += this.showTemperature;
        //TempText is set to "+ 3,6°C" at the beginning
        Temp_value = GetComponent<TextMeshProUGUI>();
        
    }

    private void showTemperature(string text)
    {
        //gets the string form the Extract Image Script and transfers it into a TestMeshProUGUI text.
        this.Temp_value.text = " " + text;
    }
    
}

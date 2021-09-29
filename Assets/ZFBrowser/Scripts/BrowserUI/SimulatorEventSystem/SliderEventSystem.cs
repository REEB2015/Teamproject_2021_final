using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class SliderEventSystem : MonoBehaviour
{
    public delegate void BrowserSliderEvent(float percentage, MouseClickRobot.PROXY_TYPE type);
    public static BrowserSliderEvent aBrowserSliderEvent;

    public delegate void TemperatureEvent(string text);
    public static TemperatureEvent aTemperatureEvent;
}



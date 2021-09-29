using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseClickRobot : MonoBehaviour
{
    //transfers the percentage value of a slider to its Proxy on the BrowserPlugIn Window

    //different Proxy types
    public enum PROXY_TYPE
    {
        NONE,
        TEST,
        INITIAL,
        TRANSPORT_ELEC,
        TRANSPORT_EFFIC,
        ENERGY_COAL,
        LAND_METHANE,
        ENERGY_RENEWABLES,
        ENERGY_CARBON,
        BUILDING_ENERGY,
        GROWTH_ECONOMIC,
        GROWTH_POPULATION,
        LeftGraph1,
        LeftGraph2,
        LeftGraph3,
        RightGraph1,
        RightGraph2,
        RightGraph3
    }

    public PROXY_TYPE proxyType;

    public Slider Slider;

    public float MinX = -1.0f;

    public float MaxX = -1.0f;

    private float initialX;

    public PointerUIBase BrowserProxy; 

    public ExtractImage Image;

    void Start()
    {
        this.initialX = this.transform.localPosition.x;
        //notices changes in the SliderEvent (when slider moves) and starts the setPercentageNew method  
        SliderEventSystem.aBrowserSliderEvent += this.setPercentageNew;
    }
    
    //takes the silderpercentage and changes the corresponding Proxy in the BrowserPlugIn 
    public void setPercentage(float percentage)
    {
        if (this.MaxX != -1.0f && this.MinX != -1.0f)
        {
            this.transform.localPosition = new Vector3(this.MinX + (this.MaxX - this.MinX) * (percentage / 100) , this.transform.localPosition.y, this.transform.localPosition.z);
            if (this.transform.localPosition.x < this.MinX)
            {
                this.transform.localPosition = new Vector3(this.MinX, this.transform.localPosition.y, this.transform.localPosition.z);
            }
            if (this.transform.localPosition.x > this.MaxX)
            {
                this.transform.localPosition = new Vector3(this.MaxX, this.transform.localPosition.y, this.transform.localPosition.z);
            }
        }
        //sets the relevantProxyType from the PointerUiBase Script to the chosen proxyType
        this.BrowserProxy.relevantProxyType = this.proxyType;

        //starts the Coroutines from the Extract Image Script to get the Temperature, right Graph and left Graph
        StartCoroutine(Image.fetchTemperaturePrediction());
        StartCoroutine(Image.fetchRightGraph(1));
        StartCoroutine(Image.fetchLeftGraph(0));
    }

    //takes the sliderpercentage and the proxytyp to start setPercentage method
    public void setPercentageNew(float percentage, MouseClickRobot.PROXY_TYPE type) 
    {
        if(this.proxyType == type)
        {
            this.setPercentage(percentage);
        }
    }
}

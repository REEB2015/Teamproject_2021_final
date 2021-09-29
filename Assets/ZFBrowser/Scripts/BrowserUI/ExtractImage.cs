using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class ExtractImage : MonoBehaviour
{
    //Extracts the Image from the webside 

    public Browser browser;

    public Browser DisplayLeft;

    public Browser DisplayRight;

    public PointerUIBase Proxy;

    
    void Start() {
        //start Coroutine to get the right graph
        StartCoroutine(this.GetTheRightGraph());
    }
    
    public IEnumerator GetTheRightGraph() {
        
        //simulates the clicks on the BrowserPlugIn to get right graph

        //waiting for 10sec to let the BrowserPlugIn load 
        yield return new WaitForSeconds(20f);
        //sets the relevant Proxy from the PointerUiBase Script to the right Proxy 
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.INITIAL;
        Debug.Log("Get The Right Graph Now!!");
        yield return new WaitForSeconds(2f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.LeftGraph1;
        yield return new WaitForSeconds(2f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.LeftGraph2;
        yield return new WaitForSeconds(2f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.LeftGraph3;
        yield return new WaitForSeconds(2f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.RightGraph1;
        yield return new WaitForSeconds(2f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.RightGraph2;
        yield return new WaitForSeconds(2f);
        Debug.Log("The last one!");
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.RightGraph3;

    }
    
    
    public IEnumerator fetchTemperaturePrediction()
    {
        //gets the temperatur predicton of the Enroad-Simulator 
        var promise = this.browser.EvalJS("document.getElementsByClassName(\"primary-temp-value\")[0].innerHTML");
        yield return promise.ToWaitFor();
        Debug.Log("promised value: " + promise.Value);
        //updates a change in "promis" to the SliderEventSystem 
        SliderEventSystem.aTemperatureEvent(promise.Value);
    }
    

    public IEnumerator fetchRightGraph(int index)
    {
        //gets the graph on the right side of the Enroad-Simulator 
        var promise = this.browser.EvalJS("document.getElementsByClassName(\"chartjs-render-monitor\")[" + index + "].toDataURL(\"img/png\")");
        yield return promise.ToWaitFor();
        Debug.Log("promised value: " + promise.Value);
        this.DisplayRight.EvalJS("document.getElementById(\"image-container\").src = '" + promise.Value + "';");
        this.DisplayRight.EvalJS("document.getElementById(\"image-container\").width = '" + 500 + "';");
        this.DisplayRight.EvalJS("document.getElementById(\"image-container\").height = '" + 500 + "';");
    }

    public IEnumerator fetchLeftGraph(int index)
    {
        //gets the graph on the left side of the Enroad-Simulator 
        var promise = this.browser.EvalJS("document.getElementsByClassName(\"chartjs-render-monitor\")[" + index + "].toDataURL(\"img/png\")");
        yield return promise.ToWaitFor();
        Debug.Log("promised value: " + promise.Value);
        this.DisplayLeft.EvalJS("document.getElementById(\"image-container\").src = '" + promise.Value + "';");
        this.DisplayLeft.EvalJS("document.getElementById(\"image-container\").width = '" + 500 + "';");
        this.DisplayLeft.EvalJS("document.getElementById(\"image-container\").height = '" + 500 + "';");
    }

}

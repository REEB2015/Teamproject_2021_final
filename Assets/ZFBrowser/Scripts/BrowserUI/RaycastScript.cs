using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;
using UnityEngine.UI;

public class RaycastScript : MonoBehaviour
{

    public PointerUIBase Proxy;

    private GameObject hitObject;

    public ExtractImage Image;
    void Update()
    {
        // mouse down
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform)
                {
                    //PrintName(hit.transform.gameObject);
                    this.hitObject = hit.transform.gameObject;
                    CurrentClickedGameObject(hit.transform.gameObject);
                }   
            }
        }
        // mouse up
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                // perform a specified action on the object that was hit last
                CurrentReleasedObject(hitObject);

                // different method, where it´s checked first wether hit.transform exists (hit.transform != null)
                // if(hit.transform)
                // {
                //      if(hit.transform.gameObject.tag != hitObject.tag)
                //     {
                //         Debug.Log("hit.transform != hitObject");
                //         PrintTag(hit.transform.gameObject.tag, "new hitObject");
                //         PrintTag(hitObject.tag, "hitobject");
                //         CurrentReleasedObject(hitObject);
                //     } else
                //     {
                //         Debug.Log("hit.transform == hitObject");
                //         PrintTag(hit.transform.gameObject.tag, "new hitObject");
                //         PrintTag(hitObject.tag, "hitobject");
                //         CurrentReleasedObject(hit.transform.gameObject);
                //     }
                // }
            }
        }
    }
    // method to manipulate the current clicked gameobject
    private void CurrentClickedGameObject(GameObject go)
    {
        if(go.tag == "Handle")
        {
            go.GetComponent<Image>().color = Color.white;
            go.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
        if(go.tag == "Button")
        {
            go.GetComponent<Image>().color = Color.black;
            go.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
            StartCoroutine(ResetSlider());
        }
    }
    // method to manipulate the gameobject that the mouse was released on
    private void CurrentReleasedObject(GameObject go)
    {
        if(go.tag == "Handle")
        {
        go.GetComponent<Image>().color = Color.red;
        go.GetComponentInChildren<MeshRenderer>().material.color = Color.red; 
        }
        if(go.tag == "Button")
        {
            go.GetComponent<Image>().color = Color.blue;
            go.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        }
    }

    public IEnumerator ResetSlider() {
        
        yield return new WaitForSeconds(0.5f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.TRANSPORT_ELEC;
        Debug.Log("Reset first Slider");
        yield return new WaitForSeconds(0.5f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.TRANSPORT_EFFIC;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Noch einen ");
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.ENERGY_COAL;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Und nochmal");
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.LAND_METHANE;
        yield return new WaitForSeconds(0.5f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.ENERGY_RENEWABLES;
        yield return new WaitForSeconds(0.5f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.ENERGY_CARBON;
        yield return new WaitForSeconds(0.5f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.BUILDING_ENERGY;
        yield return new WaitForSeconds(0.5f);
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.GROWTH_ECONOMIC;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Der letzte!");
        this.Proxy.relevantProxyType = MouseClickRobot.PROXY_TYPE.GROWTH_POPULATION;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Image.fetchTemperaturePrediction());
        StartCoroutine(Image.fetchRightGraph(1));
        StartCoroutine(Image.fetchLeftGraph(0));
    }
    // two helper functions to test the functionality
    private void PrintName(GameObject go)
    {
        Debug.Log(go.name);
    }
    private void PrintTag(string tag, string name)
    {
        Debug.Log("Name of " + name + ": " + tag);
    }


}

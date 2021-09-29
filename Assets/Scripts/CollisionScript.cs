using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionScript : MonoBehaviour
{
    public GameObject targetinSim;

    public GameObject targetinMus;

    public GameObject FadeScreen;

    public GameObject BlackScreenCanvas;

    public GameObject EasterEggCanvas;

    public GameObject PreEasterEggSign;

    public GameObject AfterEasterEggSign;

    public GameObject EasterEggCube;
    
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Teleportation(other));
    }

    // plays the Fade-Animation:
    // an Image turns from invincible to black to invincible to create a feeling of teleportation without a hard cut
    // takes 60 frames (1 second) in total
     public void FadingToOpaque()
    {
        FadeScreen.GetComponent<Animation>().Play("AnimFadeToOpaque");
    }
    
    public void FadingToTransparent()
    {
        FadeScreen.GetComponent<Animation>().Play("AnimFadeToTransparent");
    }

    // Coroutine for the actual teleportation/ easteregg
    IEnumerator Teleportation(Collider other) 
    {
        if(other.gameObject.CompareTag("toSimulator"))
        {
            BlackScreenCanvas.SetActive(true);
            //Debug.Log("if-case: Collision with " + other.gameObject.name);
            FadingToOpaque();
            yield return new WaitForSeconds(0.5f);
            this.transform.localPosition = new Vector3(targetinSim.transform.position.x, this.transform.position.y, targetinSim.transform.position.z + 1    );
            FadingToTransparent();
            yield return new WaitForSeconds(0.5f);
            BlackScreenCanvas.SetActive(false);

        } else if(other.gameObject.CompareTag("toMuseum"))
        {
            BlackScreenCanvas.SetActive(true);
            //Debug.Log("elseif-case: Collision with " + other.gameObject.name);
            FadingToOpaque();
            yield return new WaitForSeconds(0.5f);
            this.transform.localPosition = new Vector3(targetinMus.transform.position.x, this.transform.position.y, targetinMus.transform.position.z);
            FadingToTransparent();
            yield return new WaitForSeconds(0.5f);
            BlackScreenCanvas.SetActive(false);

        } else if(other.gameObject.CompareTag("easterEgg"))
        {
            Destroy(EasterEggCube); 
            BlackScreenCanvas.SetActive(true);
            //Debug.Log("elseif-case: Collision with " + other.gameObject.name);
            FadingToOpaque();
            PreEasterEggSign.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            FadingToTransparent();
            //yield return new WaitForSeconds(0.5f);
            // display EasterEgg
            EasterEggCanvas.SetActive(true); 
            // show EasterEgg for 10 seconds
            yield return new WaitForSeconds(10.0f);
            //FadingOut();
            EasterEggCanvas.SetActive(false);
            // destroy EasterEggCollider after 10 seconds  
            BlackScreenCanvas.SetActive(false);
            AfterEasterEggSign.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractor : MonoBehaviour
{
    //Multiple objects can be created but they are stored in the previous rays
    // code instead of a list/array here
    public GameObject rayCreateObject;

    private GameObject rayCreatePrefab;

    private void OnEnable()
    {
        rayCreatePrefab = Resources.Load("LightRayObject") as GameObject;
    }

    /// <summary>
    /// This function to be overridden based on use case
    /// </summary>
    /// <param name="lightDir"></param>
    /// <param name="hit"></param>
    /// <param name="hitColor"></param>
    public virtual void LightInteraction(Vector3 lightDir, RaycastHit hit, Color hitColor)
    {
        //Do nothing in base
    }

    /// <summary>
    /// Instantiates a new ray at hitPoint, with rotation based on newDirection
    /// </summary>
    /// <param name="newDirection"></param>
    /// <param name="hitPoint"></param>
    public void CreateNewRay(Vector3 newDirection, Vector3 hitPoint)
    {
        //Instantiate a ray object and store it
        rayCreateObject = Instantiate(rayCreatePrefab, hitPoint, Quaternion.identity);
        
        //rotate the newly created ray
        Quaternion rotation = Quaternion.LookRotation(newDirection);
        rayCreateObject.transform.rotation = rotation;

        //Turn on the line renderer stuff for more rays :)
        rayCreateObject.GetComponent<LineRendererScript>().enabled = true;
        rayCreateObject.GetComponent<LineRendererScript>().NewRayCall();
    }

    public void ChangeRayColor(Color filtColor)
    {
        rayCreateObject.GetComponent<LineRendererScript>().ChangeColor(filtColor);
    }
}

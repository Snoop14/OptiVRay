using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 lightStartPos;
    private Vector3 lightDirection;

    private GameObject nextRayObject;
    private GameObject rayCreatePrefab;
    private Color myColor;

    //Need to check if the line is hitting the object that brings the line into affect
    public GameObject causeInteractor;

    [SerializeField]
    float framesCounter = 0;

    private void OnEnable()
    {
        rayCreatePrefab = Resources.Load("LightRayObject") as GameObject;

        //Get the lineRenderer and set it up;
        line = GetComponent<LineRenderer>();
        line.enabled = true;
        line.positionCount = 2;

        lightStartPos = transform.position;
        lightDirection = transform.forward;

        myColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Reduce lag for when multiple rays are active
        if(line.positionCount == 2 && framesCounter >= 10) 
        {
            CheckRayHit(lightStartPos, lightDirection);
            framesCounter = 0;

        }
        framesCounter++;
    }

    /// <summary>
    /// This function checks if the light ray travelling from this object is hitting anything
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="direction"></param>
    void CheckRayHit(Vector3 startPos, Vector3 direction)
    {
        //Send a raycast out to hit an object
        //Enclosed room so to infinity should be fine
        if(Physics.Raycast(startPos, direction, out RaycastHit hit, Mathf.Infinity))
        {
            GameObject tempHitObject = hit.collider.gameObject;

            //if the hit object is hitting a lens of some sort
            //Lenses should have the lightInteractor component on them
            if (tempHitObject.TryGetComponent(out LightInteractor lightInteractor) && tempHitObject != causeInteractor)
            {
                Debug.Log("has interactor");
                if(nextRayObject == null && transform.childCount == 0)
                {
                    nextRayObject = Instantiate(rayCreatePrefab, transform);
                }
                else if(nextRayObject == null)
                {
                    nextRayObject = transform.GetChild(0).gameObject;
                }
                //Gets base function of any lens. Lens will handle next steps
                lightInteractor.LightInteraction(direction, hit, myColor, nextRayObject);
            }
            else if(nextRayObject != null)
            {
                nextRayObject.GetComponent<LineRendererScript>().DisableRay();
            }

            SetLineLength(hit.point);//sets the light ray length
        }
    }

    /// <summary>
    /// Sets the length of the line. 
    /// Lines should start at the transform position. 
    /// And end where they hit the next Object
    /// </summary>
    /// <param name="endPos"></param>
    void SetLineLength(Vector3 endPos)
    {
        line.positionCount = 2;
        endPos = transform.InverseTransformPoint(endPos);
        line.SetPosition(1, endPos);
    }

    public void ChangeColor(Color newColor)
    {
        myColor = newColor;
        GetComponent<Renderer>().material.color = myColor;
    }

    public void DisableRay()
    {
        Destroy(nextRayObject);
        ChangeColor(Color.white);
        enabled = false;
        try
        {
            line.positionCount = 0;
        }
        catch (NullReferenceException ex) { }
    }
}

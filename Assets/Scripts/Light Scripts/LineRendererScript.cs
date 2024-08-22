using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    private LineRenderer line;

    private GameObject nextRayObject;
    private GameObject rayCreatePrefab;
    private Color myColor;

    //Need to check if the line is hitting the object that brings the line into affect
    public Collider causeInteractor;

    [SerializeField]
    public float framesCounter = 0;

    public GameObject hitObject;

    private void OnEnable()
    {
        rayCreatePrefab = Resources.Load("LightRayObject") as GameObject;

        //Get the lineRenderer and set it up;
        line = GetComponent<LineRenderer>();
        line.enabled = true;
        line.positionCount = 2;

        myColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Reduce lag for when multiple rays are active
        if(line.positionCount == 2 && framesCounter >= 6) 
        {
            CheckRayHit(transform.position, transform.forward);
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
        //Debug.DrawRay(startPos, direction, Color.black);
        //Send a raycast out to hit an object
        //Enclosed room so to infinity should be fine
        if(Physics.Raycast(startPos, direction, out RaycastHit hit, 200f))
        {
            GameObject tempHitObject = hit.collider.gameObject;
            hitObject = tempHitObject;

            if (tempHitObject == causeInteractor.gameObject)
            {
                //Issue with raycast hitting the object that causes the new ray.
                // fixed by returning and trying again later??????!!!!!@!
                return;
            }

            //Checks if the object is the initial ray creator
            if(transform.parent.name != "LightRayStub")
            {
                //checks if the hit object is the same hit object as it's parent.
                //A direct parent and child should not be capable of hitting the same object
                if(tempHitObject == transform.parent.GetComponent<LineRendererScript>().hitObject)
                {
                    //If this bug does occur, disable the ray on the child and return
                    DisableRay();
                    return;
                }
            }

            //if the hit object is hitting a lens of some sort
            //Lenses should have the lightInteractor component on them
            if (tempHitObject.TryGetComponent(out LightInteractor lightInteractor))
            {
                if(nextRayObject == null && transform.childCount == 0)
                {
                    nextRayObject = Instantiate(rayCreatePrefab, transform);
                    nextRayObject.GetComponent<LineRendererScript>().framesCounter = framesCounter - 2;
                }
                else if(nextRayObject == null)
                {
                    nextRayObject = transform.GetChild(0).gameObject;
                }

                //Gets base function of any lens. Lens will handle next steps
                lightInteractor.LightInteraction(direction, hit, myColor, nextRayObject, line.startWidth);
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

    public void ChangeRayWidth(float _rayWidth)
    {
        line.startWidth = _rayWidth;
    }

    public void DisableRay()
    {
        Destroy(nextRayObject); //destroy the child object of this ray
        //Disable and reset values if possible
        ChangeColor(Color.white); 
        enabled = false;
        try
        {
            line.positionCount = 0;
        }
        catch (NullReferenceException) { }
    }
}

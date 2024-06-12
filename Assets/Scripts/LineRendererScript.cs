using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 lightStartPos;
    private Vector3 lightDirection;

    private GameObject hitObject;
    private GameObject nextRayObject;
    private bool lightHittingLens = false;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        lightStartPos = transform.position;
        lightDirection = transform.forward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckRayHit(lightStartPos, lightDirection);
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
            line.positionCount = 2;
            GameObject tempHitObject = hit.collider.gameObject;
            //if the light was hitting something before
            // and is now hitting something else
            if(lightHittingLens && tempHitObject != hitObject)
            {
                //Disable that objects light ray
                lightHittingLens = false;
                
                //needs to be created;
                //light on nextRayObject needs to be disabled;
            }

            //if the hit object is hitting a lens of some sort
            if(tempHitObject) //If condition here needs to be changed
            {
                lightHittingLens = true;

                //needs to be created
                //When hitting an object, another object needs to be created.
                //That object will create another light ray. 
                //And the object should be stored in nextRayObject;
            }

            hitObject = tempHitObject;
            SetLineLength(hit.point);
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
        endPos = transform.InverseTransformPoint(endPos);
        line.SetPosition(1, endPos);
    }
}

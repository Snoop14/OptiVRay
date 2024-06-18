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

    [SerializeField]
    float framesCounter = 0;

    /*
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        lightStartPos = transform.position;
        lightDirection = transform.forward;
    }
    */

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
        Debug.Log(myColor);
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
            if (tempHitObject.TryGetComponent(out LightInteractor lightInteractor))
            {
                if(nextRayObject == null)
                {
                    nextRayObject = Instantiate(rayCreatePrefab);
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
        try
        {
            line.positionCount = 0;
        }
        catch(NullReferenceException ex) {}

        Destroy(nextRayObject);
        nextRayObject = null;
        GetComponent<Renderer>().material.color = Color.white;
        this.enabled = false;
    }
}

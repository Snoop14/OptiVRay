﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractor : MonoBehaviour
{
    //Multiple objects can be created but they are stored in the previous rays
    // code instead of a list/array here
    public GameObject nextRayObject;
    public Color rayColor;
    private float rayWidth;

    /// <summary>
    /// This function to be overridden based on use case 
    /// Overrides need to call base first
    /// </summary>
    /// <param name="lightDir"></param>
    /// <param name="hit"></param>
    /// <param name="hitColor"></param>
    public virtual void LightInteraction(Vector3 lightDir, RaycastHit hit, 
                                         Color hitColor, GameObject _newRayObject, float _rayWidth)
    {
        nextRayObject = _newRayObject;
        rayColor = hitColor;
        rayWidth = _rayWidth;
    }

    /// <summary>
    /// Changes the new ray to be at hitPoint, with rotation based on newDirection
    /// </summary>
    /// <param name="newDirection"></param>
    /// <param name="hitPoint"></param>
    public void ChangeNewRay(Vector3 newDirection, Vector3 hitPoint)
    {
        //rotate the latest ray 
        Quaternion rotation = Quaternion.LookRotation(newDirection);
        nextRayObject.transform.rotation = rotation;

        //Move the hit point a little forward
        hitPoint += (rotation * Vector3.forward) * 0.015f;

        //change position of new ray object
        nextRayObject.transform.position = hitPoint;

        LineRendererScript lineRenderer = nextRayObject.GetComponent<LineRendererScript>();
        lineRenderer.enabled = true;
        lineRenderer.ChangeColor(rayColor);
        lineRenderer.ChangeRayWidth(rayWidth);
        lineRenderer.causeInteractor = gameObject.GetComponent<Collider>();
        
    }
}

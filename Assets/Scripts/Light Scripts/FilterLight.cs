﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterLight : LightInteractor
{
    //color is based on the color of the filter
    private Color myColor;

    // Start is called before the first frame update
    void Start()
    {
        myColor = GetComponent<Renderer>().material.color;
    }

    public override void LightInteraction(Vector3 lightDir, RaycastHit hit, Color hitColor, GameObject _newRayObject)
    {
        base.LightInteraction(lightDir, hit, hitColor, _newRayObject);
        Color filtColor = CheckColor(hitColor);

        //Create next ray only if filt color isn't black
        if(filtColor != Color.black)
        {
            rayColor = filtColor;
            CreateNewRay(lightDir, hit.point);
        }
    }

    /// <summary>
    /// Checks if the incoming light is of a color that
    /// should be transmitted through the filter
    /// </summary>
    /// <param name="_hitColor"></param>
    /// <returns></returns>
    private Color CheckColor(Color _hitColor)
    {
        Debug.Log(myColor.ToString());
        Color filtColor = Color.black;

        if(myColor.r == 1f && _hitColor.r == 1f)
        {
            filtColor.r = 1f;
        }
        if(myColor.g == 1f  && _hitColor.g == 1f)
        {
            filtColor.g = 1f;
        }
        if(myColor.b == 1f  && _hitColor.b == 1f)
        {
            filtColor.b = 1f;
        }
        return filtColor;
    }
}

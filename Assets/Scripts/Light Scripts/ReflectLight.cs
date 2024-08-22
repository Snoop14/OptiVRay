using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectLight : LightInteractor
{
    public override void LightInteraction(Vector3 lightDir, RaycastHit hit, Color hitColor, GameObject _newRayObject, float _rayWidth)
    {
        base.LightInteraction(lightDir, hit, hitColor, _newRayObject, _rayWidth);
        

        Vector3 newDirection = CalculateReflectedRayDirection(lightDir, hit);

        ChangeNewRay(newDirection, hit.point);

        /*
        //Get the angle between the lights direction and the objects mirror location direction
        Vector3 forwardMirror = transform.up; //mirror is at transform.up
        float angle = Vector3.Angle(lightDir, forwardMirror);

        //If hitting the front of the object
        if (angle > 0f)
        {
            Vector3 newDirection = CalculateReflectedRayDirection(lightDir, hit);

            ChangeNewRay(newDirection, hit.point);
        }
        else
        {
            //ray can still show when rotating mirro in hit range
            // so just disable ray to stop it
            nextRayObject.GetComponent<LineRendererScript>().DisableRay();
        }
        */
    }

    /// <summary>
    /// Calculate reflected direction of light ray
    /// </summary>
    /// <param name="lightDir"></param>
    /// <param name="hit"></param>
    /// <returns></returns>
    private Vector3 CalculateReflectedRayDirection(Vector3 lightDir, RaycastHit hit)
    {
        Vector3 reflectedDirection = Vector3.Reflect(lightDir, hit.normal);
        return reflectedDirection;
    }
}

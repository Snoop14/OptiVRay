﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Concave/Divergence lens
/// </summary>
public class ConcaveLight : LightInteractor
{
    public override void LightInteraction(Vector3 lightDir, RaycastHit hit, Color hitColor, GameObject _newRayObject, float _rayWidth)
    {
        base.LightInteraction(lightDir, hit, hitColor, _newRayObject, _rayWidth);

        //transform hit point from world to local space relative to the object
        Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);

        //Calculate new rays direction
        Vector3 newDirection = CalculateDivergentRayDirection(localHitPoint, lightDir);

        //Create a new Ray
        ChangeNewRay(newDirection, hit.point);
    }

    /// <summary>
    /// Calculate new direction for concave/Divergence lens 
    /// </summary>
    /// <param name="hitPoint"></param>
    /// <param name="incomingDirection"></param>
    /// <returns></returns>
    private Vector3 CalculateDivergentRayDirection(Vector3 localHitPoint, Vector3 incidentDirection)
    {
        //Calculate surface normal at the hit point in world space
        Vector3 worldNormal = transform.TransformDirection(localHitPoint.normalized);

        float maxDivergenceAngle = 70f;//Maximum divergence angle in degrees
        float maxDistance = 1.4f;

        //Calculate the distance of hitpoint from center
        // Also adjusts direction based on z scale;
        float distanceFromCenter = localHitPoint.magnitude / maxDistance * transform.localScale.z * 2f;

        //Calculate the maximum divergence direction based on distance
        Vector3 maxDivergenceDirection = Vector3.Lerp(Vector3.forward, worldNormal, distanceFromCenter);

        //Calculate the divergent direction based on the incident direction and maximum divergence// Unecessary code?
        Vector3 divergentDirection = Vector3.RotateTowards(incidentDirection, maxDivergenceDirection, Mathf.Deg2Rad * maxDivergenceAngle, 0f);

        return maxDivergenceDirection.normalized;
    }
}

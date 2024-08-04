using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Convex/Convergence lens
/// </summary>
public class ConvexLight : LightInteractor
{
    float refractiveIndex = 200000f;

    public override void LightInteraction(Vector3 lightDir, RaycastHit hit, Color hitColor, GameObject _newRayObject)
    {
        base.LightInteraction(lightDir, hit, hitColor, _newRayObject);

        //transform hit point from world to local space relative to the object
        Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);

        //Calculate new rays direction
        Vector3 newDirection = CalculateConvergentRayDirection(localHitPoint, lightDir);

        //Create a new ray
        ChangeNewRay(newDirection, hit.point);
    }

    private Vector3 CalculateConvergentRayDirection(Vector3 localHitPoint, Vector3 incidentDirection)
    {
        //Calculate surface normal at the hit point in world space
        Vector3 worldNormal = transform.TransformDirection(localHitPoint.normalized);

        float maxConvergenceAngle = 70f;//Maximum divergence angle in degrees
        float maxDistance = 1.4f;

        //Calculate the distance of hitpoint from center
        float distanceFromCenter = localHitPoint.magnitude / maxDistance;

        //Calculate the maximum divergence direction based on distance
        Vector3 maxConvergenceDirection = Vector3.Lerp(Vector3.forward, worldNormal, distanceFromCenter);

        maxConvergenceDirection.x = -maxConvergenceDirection.x;
        maxConvergenceDirection.y = -maxConvergenceDirection.y;

        //Calculate the divergent direction based on the incident direction and maximum divergence //uneccessary line?
        Vector3 convergentDirection = Vector3.RotateTowards(incidentDirection, maxConvergenceDirection, Mathf.Deg2Rad * maxConvergenceAngle, 0f);

        return maxConvergenceDirection.normalized * refractiveIndex;
    }
}

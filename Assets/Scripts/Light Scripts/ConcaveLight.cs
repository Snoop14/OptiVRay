using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcaveLight : LightInteractor
{
    public override void LightInteraction(Vector3 lightDir, RaycastHit hit, Color hitColor)
    {
        Vector3 forwardLens = transform.forward;
        Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);

        float angle = Vector3.Angle(lightDir, forwardLens);
    }
}

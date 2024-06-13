using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractor : MonoBehaviour
{
    //Multiple objects can be created but they are stored in the previous rays
    // code instead of a list/array here
    public GameObject rayCreateObject;

    private GameObject rayCreatePrefab;

    private void Start()
    {
        rayCreatePrefab = Resources.Load("LightRayObject") as GameObject;
    }

    /// <summary>
    /// This function to be overridden based on use case
    /// </summary>
    /// <param name="lightDir"></param>
    /// <param name="hit"></param>
    /// <param name="hitColor"></param>
    public virtual void LightInteraction(Vector3 lightDir, RaycastHit hit, Color hitColor)
    {
        //Do nothing in base
    }

    public void CreateNewRay(Vector3 _lightDir, float _angle, float _distRelation, Vector3 spawnPoint)
    {
        float rayAngle = _angle;

        Quaternion outDirection = Quaternion.AngleAxis(rayAngle, transform.up);

        rayCreateObject = Instantiate(rayCreatePrefab, spawnPoint, outDirection, transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractor : MonoBehaviour
{
    public GameObject rayCreateObject;

    /// <summary>
    /// This function to be overridden based on use case
    /// </summary>
    /// <param name="lightDir"></param>
    /// <param name="hit"></param>
    /// <param name="hitColor"></param>
    public virtual void LightInteraction(Vector3 lightDir, RaycastHit hit, Color hitColor)
    {

    }
}

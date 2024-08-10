using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFiltersController : MonoBehaviour
{
    [SerializeField]
    private SpawnFilterScript spawnFilt;

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.name == "redButton")
        {
            spawnFilt.AddRemoveColour(0);
        }
        else if(gameObject.name == "greenButton")
        {
            spawnFilt.AddRemoveColour(1);
        }
        else if (gameObject.name == "blueButton")
        {
            spawnFilt.AddRemoveColour(2);
        }
    }
}

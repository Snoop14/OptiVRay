using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] 
    private SpawnOpticalScrollScript spawn;

    private void OnTriggerEnter(Collider other)
    {
        
        if (gameObject.name == "SpawnButton")
        {
            spawn.SpawnPress();
        }
        else if (gameObject.name == "RightScrollButton")
        {
            
            spawn.RightPress();
        }
        else if (gameObject.name == "LeftScrollButton")
        {
            spawn.LeftPress();
        }
    }
}

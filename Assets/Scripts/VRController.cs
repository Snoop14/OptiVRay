using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class VRController : MonoBehaviour
{
    private XRController vrController;
    private XRDirectInteractor xrInteractor;

    [SerializeField]
    PlayerControls playerControl;

    private float scaleMultiplier = 1.1f;

    private bool firstPress = false;


    // Start is called before the first frame update
    void Start()
    {
        vrController = GetComponent<XRController>();
        xrInteractor = GetComponent<XRDirectInteractor>();
    }

    // Update
    void FixedUpdate()
    {
        ButtonControls();

        if (xrInteractor.interactablesSelected.Count > 0)
        {
            AnalogControls();
        }
    }

    private void ButtonControls()
    {
        //check if the primary button is pressed. And if it is then we may move the player
        bool isButtonPressed;
        if (vrController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isButtonPressed)
            && isButtonPressed)
        {
            if(!firstPress)
            {
                playerControl.SetPrevFrame();
                
            }
            
            playerControl.PlayerMovement();
        }

        firstPress = isButtonPressed;
    }

    private void AnalogControls()
    {
        Transform grabbedObject = xrInteractor.interactablesSelected[0].transform;
        if (grabbedObject.CompareTag("Resizable"))
        {
            Vector2 analogStickInput;
            if (vrController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out analogStickInput))
            {
                if(analogStickInput.y > 0)
                {
                    grabbedObject.localScale = new Vector3(grabbedObject.localScale.x, 
                                                           grabbedObject.localScale.y, 
                                                           Mathf.Clamp(grabbedObject.localScale.z * scaleMultiplier, 0.1f, 0.8f));
                }
                else if (analogStickInput.y < 0)
                {
                    grabbedObject.localScale = new Vector3(grabbedObject.localScale.x, 
                                                           grabbedObject.localScale.y,
                                                           Mathf.Clamp(grabbedObject.localScale.z / scaleMultiplier, 0.1f, 0.88f));
                }
            }
        }
    }
}

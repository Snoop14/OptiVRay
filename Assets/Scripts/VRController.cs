using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using System;

public class VRController : MonoBehaviour
{
    private XRController vrController;
    private XRDirectInteractor xrInteractor;

    [SerializeField]
    PlayerControls playerControl;

    private readonly float scaleMultiplier = 1.025f;

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
        if (vrController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isButtonPressed)
            && isButtonPressed)
        {
            if (!firstPress)
            {
                playerControl.SetPrevFrame();
            }
            playerControl.PlayerMovement();
        }

        firstPress = isButtonPressed;

        if (vrController.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressed) && pressed)
        {
            Transform grabbedObject;
            try
            {
                grabbedObject = xrInteractor.interactablesSelected[0].transform;
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }

            if (grabbedObject.CompareTag("Resizable"))
            {
                grabbedObject.localScale = new Vector3(grabbedObject.localScale.x, grabbedObject.localScale.y, 0.3f);
            }
            else if (grabbedObject.CompareTag("Light"))
            {
                LineRenderer lightRay = grabbedObject.GetChild(0).GetChild(0).GetComponent<LineRenderer>();
                lightRay.startWidth = 0.05f;
            }
        }
    }

    private void AnalogControls()
    {
        Transform grabbedObject;
        try
        {
            grabbedObject = xrInteractor.interactablesSelected[0].transform;
        }
        catch(ArgumentOutOfRangeException)
        {
            return;
        }

        if (grabbedObject.CompareTag("Resizable"))
        {
            if (vrController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 analogStickInput))
            {
                if (analogStickInput.y > 0)
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
        else if (grabbedObject.CompareTag("Light"))
        {
            LineRenderer lightRay = grabbedObject.GetChild(0).GetChild(0).GetComponent<LineRenderer>();
            if (vrController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 analogStickInput))
            {
                if (analogStickInput.y > 0)
                {
                    lightRay.startWidth = Mathf.Clamp(lightRay.startWidth * scaleMultiplier, 0.01f, 0.1f);
                }
                else if (analogStickInput.y < 0)
                {
                    lightRay.startWidth = Mathf.Clamp(lightRay.startWidth / scaleMultiplier, 0.01f, 0.1f);
                }
            }
        }
    }
}

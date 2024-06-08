using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class VRController : MonoBehaviour
{
    [SerializeField]
    private XRController vrController;

    [SerializeField]
    PlayerControls playerControl;


    // Start is called before the first frame update
    void Start()
    {
        vrController = GetComponent<XRController>();
    }

    // Update
    void FixedUpdate()
    {
        ButtonControls();
    }

    private void ButtonControls()
    {
        //check if the primary button is pressed. And if it is then we may move the player
        bool isButtonPressed;
        if (vrController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isButtonPressed)
            && isButtonPressed)
        {
            playerControl.PlayerMovement();
        }
    }
}

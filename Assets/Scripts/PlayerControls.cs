using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("PlayerObjects")]
    [SerializeField] GameObject LeftHand;
    [SerializeField] GameObject RightHand;
    [SerializeField] GameObject CameraXR;

    private Transform ForwardDirection;

    //Vector3 Positions //May not need the curr frame variables to be declared here
    //Player
    private Vector3 PlayerPrevFrame;
    private Vector3 PlayerCurrFrame;

    //Hands
    private Vector3 LeftHPrevFrame;
    private Vector3 RightHPrevFrame;
    private Vector3 LeftHCurrFrame;
    private Vector3 RightHCurrFrame;

    [Header("")]
    [SerializeField] float Speed = 60;
    private float Speed_Hand;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrevFrame = transform.position;
        LeftHPrevFrame = LeftHand.transform.position;
        RightHPrevFrame = RightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMovement()
    {
        //Code for player movement is from the lecture videos
        //Forward direction
        float yRot = CameraXR.transform.eulerAngles.y;
        ForwardDirection.eulerAngles = new Vector3 (0, yRot, 0);

        //Get current player and hand pos
        PlayerCurrFrame = transform.position;
        LeftHCurrFrame = LeftHand.transform.position;
        RightHCurrFrame = RightHand.transform.position;

        //Get Distance from previous frame
        float playerDistMoved = Vector3.Distance(PlayerCurrFrame, PlayerPrevFrame);
        float leftHandDistMoved = Vector3.Distance (LeftHCurrFrame, LeftHPrevFrame);
        float rightHandDistMoved = Vector3.Distance(RightHCurrFrame, RightHPrevFrame);

        Speed_Hand = ((leftHandDistMoved - playerDistMoved) + (rightHandDistMoved - playerDistMoved));

        RaycastHit hit;
        Transform camObj = transform.GetChild(0).Find("Main Camera");
        if(Physics.Raycast(camObj.position, camObj.forward, out hit, 0.2f)) 
        {
            //Something is in front of the user so do not move them
        }
        else if(Time.timeSinceLevelLoad > 2f && Speed_Hand > 0.1f)
        {
            //Move player
            transform.position += ForwardDirection.forward * Speed_Hand * Speed * Time.deltaTime;
        }

        //Set previous frame of player and hands for next frame;
        PlayerPrevFrame = transform.position;
        LeftHPrevFrame = LeftHand.transform.position;
        RightHPrevFrame = RightHand.transform.position;
    }
}

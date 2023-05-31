using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Locomotion : MonoBehaviour {
    private CharacterController controller;
    public Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Vector2 moveAction;
    public SteamVR_Input_Sources hand;
    public bool swinging;
    public Vector3 startPosition;




    private void Start() {
        startPosition = transform.position;
        controller = gameObject.GetComponent<CharacterController>();
        actionSet.Activate(hand);
        swinging = false;
    }

    void Update() {
        if (swinging) {
            playerVelocity.y = 0f;
            return;
        }

        if(transform.position.y < 2){
            playerVelocity = new Vector3(0,0,0);
            playerVelocity.y = 0f;
            transform.position = startPosition;
            Debug.Log("Dead!");
            return;
        }

        controller = gameObject.GetComponent<CharacterController>();

        Vector2 gamepad = moveAction[hand].axis;
       
        groundedPlayer = controller.isGrounded;
       
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        // Vector3 move = new Vector3(gamepad.x, 0, gamepad.y);
        // Quaternion rotation = Player.instance.hmdTransform.rotation;
        // controller.Move(rotation * move * Time.deltaTime * playerSpeed);

        // Simula gravidade
        playerVelocity.x =  playerVelocity.x/1.05f;
        playerVelocity.z =  playerVelocity.z/1.05f;
        playerVelocity.y += gravityValue * Time.deltaTime;
        // Debug.Log("Player Velocity" + playerVelocity);
        controller.Move(playerVelocity  * Time.deltaTime);
    }
}


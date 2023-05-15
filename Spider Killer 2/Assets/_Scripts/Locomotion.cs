using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Locomotion : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Vector2 moveAction;
    public SteamVR_Input_Sources hand;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        actionSet.Activate(hand);
    }

    void Update()
    {
    // Posição do gamepad
        Vector2 gamepad = moveAction[hand].axis;
       
        groundedPlayer = controller.isGrounded;
       
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        // Vector3 move = new Vector3(gamepad.x, 0, gamepad.y);
        // Quaternion rotation = Player.instance.hmdTransform.rotation;
        // controller.Move(rotation * move * Time.deltaTime * playerSpeed);

        // // // Simula gravidade
        // playerVelocity.y += gravityValue * Time.deltaTime;
        // controller.Move(playerVelocity * Time.deltaTime);
    }
}


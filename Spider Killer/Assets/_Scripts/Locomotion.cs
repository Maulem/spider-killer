using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Locomotion : MonoBehaviour {
    private CharacterController controller;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Vector2 moveAction;
    public SteamVR_Input_Sources hand;
    public bool swinging;
    public Vector3 startPosition;
    public Rigidbody rb;
    public Vector3 lastPos;




    private void Start() {
        startPosition = transform.position;
        controller = gameObject.GetComponent<CharacterController>();
        actionSet.Activate(hand);
        swinging = false;
        lastPos = transform.position;
    }

    void Update() {
        playerVelocity = rb.velocity;
        if (swinging) {
            playerVelocity.y = 0f;
            rb.velocity = playerVelocity;
            
            Debug.Log("Player Velocity rb:" + playerVelocity);
            // return;
        }

        if(transform.position.y < 2){
            playerVelocity = new Vector3(0,0,0);
            playerVelocity.y = 0f;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = startPosition;
            Debug.Log("Dead!");
            return;
        }

        // if (rb.velocity.x > 10) {
        //     int diff = (int)(lastPos.x - transform.position.x);
        //     if (diff < 0) {
        //         diff *= -1;
        //     }
        //     if (diff < 1) {
        //         playerVelocity.x = 0f;
        //         rb.velocity = playerVelocity;
        //         Debug.Log("Stop X!" + diff);
        //     }
        // }
        // if (rb.velocity.z > 10) {
        //     int diff = (int)(lastPos.z - transform.position.z);
        //     if (diff < 0) {
        //         diff *= -1;
        //     }
        //     if (diff < 1) {
        //         playerVelocity.z = 0f;
        //         rb.velocity = playerVelocity;
        //         Debug.Log("Stop Z!" + diff);
        //     }
        // }
        

        controller = gameObject.GetComponent<CharacterController>();

        // Vector2 gamepad = moveAction[hand].axis;
       
        groundedPlayer = controller.isGrounded;
       
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
            rb.velocity = playerVelocity;
            Debug.Log("Ground!");
        }

        // Vector3 move = new Vector3(gamepad.x, 0, gamepad.y);
        // Quaternion rotation = Player.instance.hmdTransform.rotation;
        // controller.Move(rotation * move * Time.deltaTime * playerSpeed);

        // Simula gravidade
        // playerVelocity.x =  playerVelocity.x/1.05f;
        // playerVelocity.z =  playerVelocity.z/1.05f;
        playerVelocity.y += gravityValue * Time.deltaTime;
        
        controller.Move(playerVelocity  * Time.deltaTime);
        // Debug.Log("Player Last" + lastPos);
        lastPos = transform.position;
        
        // Debug.Log("Player Velocity" + playerVelocity);
    }
}


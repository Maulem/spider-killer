using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using Valve.VR.Extras;

public class laser : MonoBehaviour {
    private SteamVR_LaserPointer steamVrLaserPointer;
    private CharacterController characterController;
    private Rigidbody rigidbody;
    public Transform Player;
    public LineRenderer lr;
    public Transform gunTip;
    
    private Vector3 swingPoint;
    private SpringJoint joint;

    private float moveSpeed = 50f; // Velocidade de movimento do jogador

    private bool isHanging = false;

    private Vector3 objectFinal;
    private Vector3 playerVelocity;

    void Update() {
        if (isHanging) {
            // MoveTowardsTarget();
        }
    }

    private void Awake() {
        Player = transform.parent.parent;
        characterController = transform.parent.parent.GetComponent<CharacterController>();
        rigidbody = transform.parent.parent.GetComponent<Rigidbody>();
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerClick += OnPointerClick;
        steamVrLaserPointer.PointerClickDown += OnPointerClickDown;
    }

    private void OnPointerClick(object sender, PointerEventArgs e) {
        Player.gameObject.GetComponent<Locomotion>().swinging = false;
        StartCoroutine(DisableHangingDelayed());
        Destroy(joint);
        if (characterController != null) {
            DestroyImmediate(characterController);
            characterController = Player.gameObject.AddComponent<CharacterController>();
        }
    }

    private void OnPointerClickDown(object sender, PointerEventArgs e) {
        Vector3 clickedPosition = e.point;
        objectFinal = clickedPosition;
        isHanging = true;
        Player.gameObject.GetComponent<Locomotion>().swinging = true;

        swingPoint = objectFinal;
        joint = Player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(Player.position, swingPoint);

        // the distance grapple will try to keep from grapple point. 
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        // customize values as you like
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lr.positionCount = 2;
        // currentGrapplePosition = gunTip.position;

        
        
        
    }

    private void LateUpdate(){
        if (isHanging) {
            lr.positionCount = 2;
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, objectFinal);
        }
        else {
            lr.positionCount = 0;
        }
    }

    private void MoveTowardsTarget() {
        Vector3 hangDirection = (objectFinal - transform.position).normalized;
        playerVelocity = hangDirection * moveSpeed;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private IEnumerator DisableHangingDelayed() {
    yield return new WaitForSeconds(0.25f); // Wait for 0.25 seconds
    Player.gameObject.GetComponent<Locomotion>().playerVelocity = playerVelocity; 
    isHanging = false;
    }
}

    
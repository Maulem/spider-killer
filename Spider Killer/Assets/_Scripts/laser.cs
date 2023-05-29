using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using Valve.VR.Extras;

public class laser : MonoBehaviour
{
    private SteamVR_LaserPointer steamVrLaserPointer;
    private CharacterController characterController;
    private Rigidbody rigidbody;
    public GameObject Player;

    private float moveSpeed = 50f; // Velocidade de movimento do jogador

    private bool isHanging = false;

    private Vector3 objectFinal;
    private Vector3 playerVelocity;
    void Update()
    {
        if (isHanging)
        {
            MoveTowardsTarget();
        }
    }




    private void Awake()
    {
        Player = transform.parent.parent.gameObject;
        characterController = transform.parent.parent.GetComponent<CharacterController>();
        rigidbody = transform.parent.parent.GetComponent<Rigidbody>();
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerClick += OnPointerClick;
        steamVrLaserPointer.PointerClickDown += OnPointerClickDown;
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        Player.GetComponent<Locomotion>().swinging = false;
        StartCoroutine(DisableHangingDelayed());
    }

    private void OnPointerClickDown(object sender, PointerEventArgs e)
    {

        Vector3 clickedPosition = e.point;
        objectFinal = clickedPosition;
        isHanging = true;
        Player.GetComponent<Locomotion>().swinging = true;
    }



    private void MoveTowardsTarget()
    {
        Vector3 hangDirection = (objectFinal - transform.position).normalized;
        playerVelocity = hangDirection * moveSpeed ;
        
        characterController.Move(playerVelocity * Time.deltaTime);
    }


    private IEnumerator DisableHangingDelayed()
    {
    yield return new WaitForSeconds(0.25f); // Wait for 0.25 seconds
    Player.GetComponent<Locomotion>().playerVelocity =playerVelocity; 
    isHanging = false;
    }


}

    
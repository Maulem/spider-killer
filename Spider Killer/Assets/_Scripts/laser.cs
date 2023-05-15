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

    private float moveSpeed = 25f; // Velocidade de movimento do jogador

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
        characterController = transform.parent.parent.GetComponent<CharacterController>();
        rigidbody = transform.parent.parent.GetComponent<Rigidbody>();
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
        steamVrLaserPointer.PointerClickDown += OnPointerClickDown;
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        
        StartCoroutine(DisableHangingDelayed());
    }


    private void OnPointerOut(object sender, PointerEventArgs e)
    {
    Debug.Log("laser saiu do objeto " + e.target.name);
    }

private void OnPointerIn(object sender, PointerEventArgs e)
    {
        Debug.Log("laser entrou do objeto " + e.target.name);
    }

    private void OnPointerClickDown(object sender, PointerEventArgs e)
    {
    //     RaycastHit hit = e.targetHit;
    //     if (hit.collider != null)
    // {
    //     Vector3 clickedPosition = hit.point; // Get the point of the hit
    //     objectFinal = clickedPosition;
    //     isHanging = true;

    //     Debug.Log("Clicked Down");
    // }

        GameObject clickegObject = e.target.gameObject;
        Vector3 clickedPosition = clickegObject.transform.position;
        objectFinal = clickedPosition;
        isHanging = true;

        Debug.Log("Clicked Down");
    }


    private void HangOnWeb(Vector3 webPoint)
    {
        isHanging = true;

    }

    private void MoveTowardsTarget()
    {
        Vector3 hangDirection = (objectFinal - transform.position).normalized;
        // Vector3 force = hangDirection * moveSpeed;
        // rigidbody.AddForce(force*1000);
        playerVelocity = hangDirection * moveSpeed * Time.deltaTime;
        characterController.Move(playerVelocity);
    }


    private IEnumerator DisableHangingDelayed()
    {
    yield return new WaitForSeconds(0.25f); // Wait for 1.5 seconds
    isHanging = false;
    }


}

    
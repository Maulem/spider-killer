using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using Valve.VR.Extras;

public class laser : MonoBehaviour
{
    private SteamVR_LaserPointer steamVrLaserPointer;
    private CharacterController characterController;
    public GameObject Player;
    private Rigidbody rb;
    private HingeJoint hJoint;

     

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
        steamVrLaserPointer.PointerClickDown += OnPointerClickDown;
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        // GameObject clickegObject = e.target.gameObject;
        // Vector3 clickedPosition = clickegObject.transform.position;
        // rb = clickegObject.GetComponent<Rigidbody>();
        // Debug.Log(rb);
        // // HangOnWeb(clickedPosition);
        // if (hJoint == null) {
        //     hJoint = Player.AddComponent<HingeJoint>();
        //     hJoint.connectedBody = rb;
        // }
        // else {
        //     Destroy(hJoint);
        //     hJoint = Player.AddComponent<HingeJoint>();
        //     hJoint.connectedBody = rb;
        // }

        Player.GetComponent<SwingingDone>().StopSwing();


        Debug.Log("objeto clicado com o laser " + e.target.name);
        // Debug.Log("Clicked object position: "+ clickedPosition);
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

        // GameObject clickegObject = e.target.gameObject;
        // Vector3 clickedPosition = clickegObject.transform.position;
        // objectFinal = clickedPosition;
        // isHanging = true;
        Player.GetComponent<SwingingDone>().StartSwing();

        Debug.Log("Clicked Down");
    }

    private void OnPointerOut(object sender, PointerEventArgs e)
    {
        // Debug.Log("laser saiu do objeto " + e.target.name);
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
        {
            // Debug.Log("laser entrou do objeto " + e.target.name);
        }

    private void HangOnWeb(Vector3 webPoint)
        {
            
        }
}

    
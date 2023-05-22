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

    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;
    public LayerMask whatIsGrappleable;

    [Header("Swinging")]
    private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;
    public Transform gunTip;

    public LineRenderer lr;
    public PlayerMovementGrappling pm;
    private Vector3 currentGrapplePosition;
     

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
        Debug.Log("Clicked down with event e position" + e.target.gameObject.transform.position);
        Player.GetComponent<SwingingDone>().StartSwing(e.target.gameObject.transform.position);
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

    public void CheckForSwingPointsHand(){
        RaycastHit sphereCastHit;
        Physics.SphereCast(transform.position, predictionSphereCastRadius, transform.forward, 
                            out sphereCastHit, maxSwingDistance, whatIsGrappleable);
        
        RaycastHit raycastHit;
        Physics.Raycast(transform.position, transform.forward, 
                            out raycastHit, maxSwingDistance, whatIsGrappleable);
        Vector3 realHitPoint;

            // Option 1 - Direct Hit
            if (raycastHit.point != Vector3.zero)
                realHitPoint = raycastHit.point;

            // Option 2 - Indirect (predicted) Hit
            else if (sphereCastHit.point != Vector3.zero)
                realHitPoint = sphereCastHit.point;

            // Option 3 - Miss
            else
                realHitPoint = Vector3.zero;

            // realHitPoint found
            if (realHitPoint != Vector3.zero)
            {
                predictionPoint.gameObject.SetActive(true);
                predictionPoint.position = realHitPoint;
            }
            // realHitPoint not found
            else
            {
                predictionPoint.gameObject.SetActive(false);
            }

            predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
        }

        public void StartSwing(Vector3 predictionHitPoint)
    {
        Debug.Log("StartSwing foi chamado com o prediction" + predictionHitPoint);
        // return if predictionHit not found
        if (predictionHitPoint == Vector3.zero) return;

        // deactivate active grapple
        if(GetComponent<Grappling>() != null)
            GetComponent<Grappling>().StopGrapple();
        pm.ResetRestrictions();

        pm.swinging = true;

        swingPoint = predictionHitPoint;
        joint = gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

        // the distance grapple will try to keep from grapple point. 
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        // customize values as you like
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lr.positionCount = 2;
        currentGrapplePosition = gunTip.position;
    }

    public void StopSwing()
    {
        pm.swinging = false;

        lr.positionCount = 0;

        Destroy(joint);
    }
}

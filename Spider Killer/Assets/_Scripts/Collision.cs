using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

    public Rigidbody rb;

    void OnTriggerEnter(Collider a) { 
        if (a.gameObject.tag != "Coin") {
            Debug.Log("Colision!");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    } 
}

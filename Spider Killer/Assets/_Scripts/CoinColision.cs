using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinColision : MonoBehaviour {
    void OnTriggerEnter(Collider a) {  
        Debug.Log("Coin collected!");
        Destroy(gameObject);
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour {
    float speed = 25f;
    void Update () {
        transform.Rotate(Vector3.right *speed * Time.deltaTime);
    }
}

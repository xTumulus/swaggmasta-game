using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    [Header("Inscribed")]
    public Vector3 destination;

    void OnCollision2DEnter(Collision2D collision) {
        if (destination == null) {
            Debug.Log("Location for teleporter " + gameObject.name + " has not been set.");
            return;
        }
        
        collision.gameObject.transform.position = destination;
    }

}

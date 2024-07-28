using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosed : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) {
        // Debug.Log("Door collided with " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<Swagg>() != null) {
            Destroy(gameObject);
        }
    }

    // TODO need a method to open when all enemies are dead (base on room number?)
}

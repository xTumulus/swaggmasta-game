using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DoorClosed : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Door collided with " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<Swagg>() != null) {
            gameObject.active = false;
        }
    }
}

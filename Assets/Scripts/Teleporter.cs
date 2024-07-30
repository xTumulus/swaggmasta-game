using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    [Header("Inscribed")]
    public Teleporter destination;
    public int destinationXOffset = 0;
    public int destinationYOffset = 0;

    void OnCollisionEnter2D(Collision2D collision) {
        if (destination == null) {
            Debug.Log("Location for teleporter " + gameObject.name + " has not been set.");
            return;
        }

        Debug.Log(collision.gameObject.name + " collided with " + gameObject.name);

        // Only player can teleport
        string otherLayer = LayerMask.LayerToName(collision.gameObject.layer);
        if (otherLayer != "Swagg") return;
        
        Vector3 destinationPosition = destination.gameObject.transform.position;
        collision.gameObject.transform.position = new Vector3(destinationPosition.x + destinationXOffset, destinationPosition.y + destinationYOffset, destinationPosition.x);
    }

}

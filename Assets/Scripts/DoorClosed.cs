using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosed : MonoBehaviour {

    [Header("Inscribed")]
    public Room room;
    public bool mustClearEnemies = false;

    void Update() {
        bool openDoor = false;

        if (mustClearEnemies) {
            // Debug.Log("Must clear enemies in " + room.gameObject.name);
            openDoor = true;
            GameObject go;
            foreach(Transform child in room.transform) {
                go = child.gameObject;
                // Debug.Log("child is " + go.name);
                string childLayer = LayerMask.LayerToName(go.layer);
                if (childLayer == "Enemies") {
                    openDoor = false;
                }
            }
        }

        if (openDoor) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (mustClearEnemies) return;
        // Debug.Log("Door collided with " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<Swagg>() != null) {
            Destroy(gameObject);
        }
    }
}

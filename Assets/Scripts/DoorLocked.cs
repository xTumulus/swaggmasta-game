using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D collision) {
        // Debug.Log(collision.gameObject.name + " collided with " + gameObject.name);
        if (collision.gameObject.GetComponent<Swagg>() != null) {
            CheckKeys(collision);
        }
    }

    private void CheckKeys(Collision2D collision) {

        Debug.Log(gameObject.name + "collided with " + gameObject.name);
        IKeyMaster keyMaster = collision.gameObject.GetComponent<IKeyMaster>();
        if (keyMaster == null) return;

        if (keyMaster.dungeonKeys > 0) {
            keyMaster.dungeonKeys--;
            Destroy(gameObject);
        }
    }

}

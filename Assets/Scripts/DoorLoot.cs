using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLoot: MonoBehaviour
{
    void OnCollisionStay2D(Collision2D collision) {
        // Debug.Log(collision.gameObject.name + " collided with " + gameObject.name);
        if (collision.gameObject.GetComponent<Swagg>() != null) {
            CheckKeys(collision);
        }
    }

    private void CheckKeys(Collision2D collision) {
        IKeyMaster keyMaster = collision.gameObject.GetComponent<IKeyMaster>();
        if (keyMaster == null) return;

        if (keyMaster.lootKeys > 0) {
            keyMaster.lootKeys--;
            Destroy(gameObject);
        }
    }

}

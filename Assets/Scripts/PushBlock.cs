using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour {

    [Header("Inscribed")]
    public int movesLimit = 1;
    public int[] allowedFacings;
    public Vector3 correctPosition;

    [Header("Dynamic")]
    public int totalMoves = 0;
    public bool inPlace = false;

    public void Update() {
        if (transform.position == correctPosition) {
            inPlace = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {

        Debug.Log(collision.gameObject.name + " collided with " + gameObject.name);

        // Only player can move block
        string otherLayer = LayerMask.LayerToName(collision.gameObject.layer);
        if (otherLayer != "Swagg") return;

        if (totalMoves == movesLimit) return;

        IFacingMover collisionMover = collision.gameObject.GetComponent<IFacingMover>();
        if (collisionMover == null) return;

        int pusherFacing = collisionMover.GetFacing();
        Debug.Log(collision.gameObject.name + " facing " + pusherFacing);
        if (allowedFacings.Contains(collisionMover.GetFacing())) {
            int xMovement = 0;
            int yMovement = 0;

            switch(pusherFacing) {
                case 0:
                    xMovement += 1;
                    break;
                case 1:
                    yMovement += 1;
                    break;
                case 2:
                    xMovement -= 1;
                    break;
                case 3:
                    yMovement -= 1;
                    break;
            }

            transform.position = new Vector3(transform.position.x + xMovement, transform.position.y + yMovement, transform.position.z);
            totalMoves++;
        }
    }
}

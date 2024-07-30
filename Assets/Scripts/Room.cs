using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    [Header("Inscribed")]
    public GameObject overlayText;
    public GameObject[] enemies;

    // Start is called before the first frame update
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);

        overlayText.SetActive(true);
        foreach (GameObject enemy in enemies) {
            Instantiate<GameObject>(enemy);
            // go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        
    }


}

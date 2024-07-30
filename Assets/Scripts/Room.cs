using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    [Header("Inscribed")]
    public GameObject overlayText;
    public List<GameObject> enemies;
    public bool respawnEnemies = false;
    public float maxRoomX = 16f;
    public float maxRoomY = 11f;
    public float wallThinkness = 2f;

    private bool spawnedOnce = false;

    protected void OnCollisionEnter2D(Collision2D collision) {
        // Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
    }

    protected void OnTriggerEnter2D(Collider2D collider) {

        Debug.Log(collider.gameObject.name + " entered " + gameObject.name);

        if (overlayText != null) {
            overlayText.SetActive(true);
        }
        if (respawnEnemies) {
            SpawnEnemies();
        } else if (!spawnedOnce) {
            SpawnEnemies();
            spawnedOnce = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collider) {

        Debug.Log(collider.gameObject.name + " left " + gameObject.name);

        if (respawnEnemies) {
            Debug.Log("Destroying all enemies in " + gameObject.name);
            foreach(Transform child in this.transform) {
                GameObject go = child.gameObject;
                string childLayer = LayerMask.LayerToName(go.layer);
                if (childLayer == "Enemies") {
                    Destroy(child.gameObject);
                }
            }
        }

        if (overlayText != null) {
            overlayText.SetActive(false);
        }
    }

    private void SpawnEnemies() {

        List<Vector3> spawnPoints = getSpawnPoints();

        for (int i = 0; i < enemies.Count; i++) {
            GameObject go = Instantiate<GameObject>(enemies[i], this.transform);
            go.transform.position = spawnPoints[i];
        }
    }

    private List<Vector3> getSpawnPoints() {
        List<Vector3> spawnPoints = new List<Vector3>();

        for (int i = 0; i < enemies.Count; i++) {
            float x = Random.Range(0 + wallThinkness, maxRoomX - wallThinkness);
            float y = Random.Range(0 + wallThinkness, maxRoomY - wallThinkness);
            spawnPoints.Add(new Vector3(this.transform.position.x + x, this.transform.position.y + y, 0));
        }

        return spawnPoints;
    }
}

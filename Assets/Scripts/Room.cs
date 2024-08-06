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
    public float wallThickness = 2f;
    public float respawnTime = 20;

    private float nextRespawn;
    private bool spawnedOnce = false;

    protected void OnCollisionEnter2D(Collision2D collision) {
        // Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
    }

    protected void OnTriggerEnter2D(Collider2D collider) {

        Debug.Log(collider.gameObject.name + " entered " + gameObject.name);

        if (overlayText != null) {
            overlayText.SetActive(true);
        }
        if (respawnEnemies && nextRespawn < Time.time) {
            DestroyEnemies();
            SpawnEnemies();
        } else if (!respawnEnemies && !spawnedOnce) {
            SpawnEnemies();
            spawnedOnce = true;
        }

        nextRespawn = Time.time + respawnTime;
        Debug.Log("Time: " + Time.time);
        Debug.Log("Respawn Time: " + nextRespawn);
    }

    protected void OnTriggerExit2D(Collider2D collider) {

        Debug.Log(collider.gameObject.name + " left " + gameObject.name);

        if (overlayText != null) {
            overlayText.SetActive(false);
        }
    }

    private void DestroyEnemies() {

        Debug.Log("Destroying all enemies in " + gameObject.name);
        foreach(Transform child in this.transform) {
            GameObject go = child.gameObject;
            string childLayer = LayerMask.LayerToName(go.layer);
            if (childLayer == "Enemies") {
                Destroy(child.gameObject);
            }
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
            float x = Random.Range(0 + wallThickness, maxRoomX - wallThickness);
            float y = Random.Range(0 + wallThickness, maxRoomY - wallThickness);
            spawnPoints.Add(new Vector3(this.transform.position.x + x, this.transform.position.y + y, 0));
        }

        return spawnPoints;
    }
}

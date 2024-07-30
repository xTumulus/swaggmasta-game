using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public enum eType { health, money, moneyBig, key, compass, map, grappler, sword, hoodie, shield, bow }
    public static float COLLIDER_DELAY = 0.5f;

    [Header("Inscribed")]
    public eType itemType;
    public int cost = 0;

    // Awake() and Activate() disable the PickUp's Collider for 0.5 secs
    void Awake() {
        GetComponent<Collider2D>().enabled = false;
        Invoke("Activate", COLLIDER_DELAY);
    }
    void Activate()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}

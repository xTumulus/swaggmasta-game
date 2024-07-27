using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum eType { health, money, key, grappler }
    public static float COLLIDER_DELAY = 0.5f;

    [Header("Inscribed")]
    public eType itemType;

    // Awake() and Activate() disable the PickUp's Collider for 0.5 secs
    void Awake()
    {
        GetComponent<Collider>().enabled = false;
        Invoke("Activate", COLLIDER_DELAY);
    }
    void Activate()
    {
        GetComponent<Collider>().enabled = true;
    }
}

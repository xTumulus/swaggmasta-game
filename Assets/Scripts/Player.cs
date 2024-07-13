using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player playerSingleton;

    //Movement
    public float speed = 20;

    void Awake() {
        playerSingleton = this;
    }

}

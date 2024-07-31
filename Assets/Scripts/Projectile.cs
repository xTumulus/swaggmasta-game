using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IFacingMover {

    [Header("Inscribed")]
    public float speed = 5;

    public Rigidbody2D rigid;
    private Collider2D collider;
    private int facing = 0;

    private InRoom inRm;

    void Awake() {
      rigid = GetComponent<Rigidbody2D>();
      collider = GetComponent<Collider2D>();
      inRm = GetComponent<InRoom>();
    }

    public void Shoot(int shooterFacing, Vector2 direction) {
      facing = shooterFacing;
      gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
      // Debug.Log("Projectile collision with " + collision.gameObject.name);
      Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      // Debug.Log("Projectile triggered with " + collider.gameObject.name);
      Destroy(this.gameObject);
    }

    public int GetFacing(){
        return facing % 4;
    }

    public float GetSpeed() {
        return speed;
    }

    public bool moving {
        get { return(facing < 4); }
    }

    public float gridMult {
        get { return inRm.gridMult; }
    }

    public bool isInRoom {
        get { return inRm.isInRoom; }
    }

    public Vector2 roomNum {
        get { return inRm.roomNum; }
        set { inRm.roomNum = value; }
    }

    public Vector2 posInRoom {
        get { return inRm.posInRoom; }
        set { inRm.posInRoom = value; }
    }

    public Vector2 GetGridPosInRoom(float mult = -1) {
        return inRm.GetGridPosInRoom(mult);
    }
}

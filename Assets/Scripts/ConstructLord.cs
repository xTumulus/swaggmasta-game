using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(InRoom) )]
public class ConstructLord : Enemy, IFacingMover {

    [Header("Inscribed: Construct")]
    public int speed = 4;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;

    [Header("Dynamic: Construct")]
    [Range(0,4)]
    public int facing = 0;
    public float timeNextDecision = 0;

    private InRoom inRm;

    protected override void Awake() {
        base.Awake();
        inRm = GetComponent<InRoom>();
    }

    protected override void Update() {

        base.Update();
        if (knockback) return;

        if (Time.time >= timeNextDecision) {
            DecideDirection();
        }

        rigid.velocity = directions[facing] * speed;
    }

    protected override void Die() {
        Debug.Log(gameObject.name + " is dead.");

        GameObject go;
        if (guaranteedItemDrop != null) {
            go = Instantiate<GameObject>(guaranteedItemDrop);
            go.transform.position = transform.position;
        } else if (randomItemDrops.Length > 0) {
            int n = Random.Range(0, randomItemDrops.Length);
            GameObject prefab = randomItemDrops[n];
            if (prefab != null) {
                go = Instantiate<GameObject>(prefab);
                go.transform.position = transform.position;
            }
        }

        Destroy(transform.parent.gameObject);
    }

    void DecideDirection() {
        facing = Random.Range(0, 4);
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
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

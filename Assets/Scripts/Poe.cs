using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poe : Enemy, IFacingMover {
    [Header("Set in Inspector: Poe")]
    [SerializeField] private int speed = 4;
    [SerializeField] private float timeThinkMin = 0.6f;
    [SerializeField] private float timeThinkMax = 1.3f;

    [Header("Set Dynamically: Poe")]
    [SerializeField] private int facing ;
    [SerializeField] private float timeNextDecision;

    private InRoom inRm;
    private const int MinSpeed = 0;
    private const int MaxSpeed = 4;

    protected override void Awake()
    {
        base.Awake();
        inRm = GetComponent<InRoom>();
    }

    protected override void Update()
    {
        base.Update();
        if (knockback) return;
        if (stun) {
            speed = MinSpeed;
            rigid.velocity = directions[facing] * speed;
            return;
        }
        else
        {
            speed = MaxSpeed;
        }


        if (Time.time >= timeNextDecision)
        {
            DecideDirection();
        }

        rigid.velocity = directions[facing] * speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        string otherLayer = LayerMask.LayerToName(collision.gameObject.layer);
        if (otherLayer != "Grappler") return;

        base.OnTriggerEnter2D(collision);
    }

    void DecideDirection() {
        facing = Random.Range(0, 4);
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }

    // IFacingMover
    public int GetFacing() {
        return facing;
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

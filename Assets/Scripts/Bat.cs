using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    // [Header("Set in Inspector: Bat")]
    // [SerializeField] private int speed = 4;
    // [SerializeField] private float timeThinkMin = 0.6f;
    // [SerializeField] private float timeThinkMax = 1.3f;
    //
    // [Header("Set Dynamically: Bat")]
    // [SerializeField] private int facing ;
    // [SerializeField] private float timeNextDecision;
    //
    // private InRoom _inRm;
    // private const int MinSpeed = 0;
    // private const int MaxSpeed = 4;
    //
    // protected override void Awake()
    // {
    //     base.Awake();
    //     _inRm = GetComponent<InRoom>();
    // }
    //
    // protected override void Update()
    // {
    //     base.Update();
    //     if (knockback) return;
    //     if (stun)
    //     {
    //         speed = MinSpeed;
    //         rigid.velocity = Directions[facing] * speed;
    //         return;
    //     }
    //     else
    //     {
    //         speed = MaxSpeed;
    //     }
    //
    //
    //     if (Time.time >= timeNextDecision)
    //     {
    //         DecideDirection();
    //     }
    //
    //     rigid.velocity = Directions[facing] * speed;
    // }
    //
    // void DecideDirection()
    // {
    //     facing = Random.Range(0, 4);
    //     anim.CrossFade("Bat_" + facing, 0);
    //     timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    // }
    //
    // // IFacingMover
    // public int GetFacing()
    // {
    //     return facing;
    // }
    //
    // public bool Moving => (true);
    //
    // public float GetSpeed()
    // {
    //     return speed;
    // }
    //
    // public float GridMult => _inRm.gridMult;
    //
    // public Vector2 RoomPos
    // {
    //     get => _inRm.RoomPos;
    //     set => _inRm.RoomPos = value;
    // }
    //
    // public Vector2 RoomNum
    // {
    //     get => _inRm.RoomNum;
    //     set => _inRm.RoomNum = value;
    // }
    //
    // public Vector2 GetRoomPosOnGrid(float mult = -1)
    // {
    //     return _inRm.GetRoomPosOnGrid(mult);
    // }
}

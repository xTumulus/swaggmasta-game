using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    // [Header("Set in Inspector: Slime")]
    // [SerializeField] private int speed = 1;
    // [SerializeField] private float timeThinkMin = 2f;
    // [SerializeField] private float timeThinkMax = 4f;
    //
    // [Header("Set Dynamically: Slime")]
    // [SerializeField] private int facing;
    // [SerializeField] private float timeNextDecision;
    //
    // private InRoom _inRm;
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
    //     if (stun) {
    //         speed = 0;
    //         rigid.velocity = Directions[facing] * speed;
    //         return;
    //     } else {
    //         speed = 1;
    //     }
    //
    //     if (Time.time >= timeNextDecision) {
    //         DecideDirection();
    //     }
    //
    //     rigid.velocity = Directions[facing] * speed;
    // }
    //
    // private void DecideDirection() {
    //     facing = Random.Range(0, 4); // Случайное направление
    //     timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    // }
    //
    // // IFacingMover
    // public int GetFacing() {
    //     return facing;
    // }
    //
    // public bool Moving => (true);
    //
    // public float GetSpeed() {
    //     return speed;
    // }
    //
    // public float GridMult => _inRm.gridMult;
    //
    // public Vector2 RoomPos {
    //     get => _inRm.RoomPos;
    //     set => _inRm.RoomPos = value;
    // }
    //
    // public Vector2 RoomNum {
    //     get => _inRm.RoomNum;
    //     set => _inRm.RoomNum = value;
    // }
    //
    // public Vector2 GetRoomPosOnGrid(float mult = -1) {
    //     return _inRm.GetRoomPosOnGrid(mult);
    // }
}

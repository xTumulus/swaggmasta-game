using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IFacingMover

{
    int GetFacing();
    float GetSpeed();
    bool moving { get; }
    float gridMult { get; }
    Vector2 roomNum { get; set; }
    Vector2 posInRoom { get; set; }
    Vector2 GetGridPosInRoom(float mult = -1);
}

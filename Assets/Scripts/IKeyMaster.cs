using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IKeyMaster

{
    int dungeonKeys { get; set; }
    int lootKeys { get; set; }
    bool hasBossKey { get; }
    Vector2 position { get; }
    int GetFacing();
}

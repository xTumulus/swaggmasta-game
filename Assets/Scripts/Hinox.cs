using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(InRoom) )]
public class Hinox : Enemy, IFacingMover {

    [Header("Inscribed: Hinox")]
    public int speed = 2;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;
    public float damageColliderExpansion = 0.25f;

    [Header("Dynamic: Hinox")]
    [Range(0,4)]
    public int facing = 0;
    public float timeNextDecision = 0;

    private InRoom inRm;
    private DamageEffect damageEffect;
    private BoxCollider2D damageCollider;

    protected override void Awake() {
        base.Awake();
        inRm = GetComponent<InRoom>();
        damageEffect = GetComponent<DamageEffect>();
        damageCollider = GetComponent<BoxCollider2D>();
    }

    protected override void Update() {

        base.Update();
        if (knockback) return;

        if (Time.time >= timeNextDecision) {
            DecideDirection();
        }

        rigid.velocity = directions[facing] * speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {

        // Can't be damaged by the grappler
        string otherLayer = LayerMask.LayerToName(collision.gameObject.layer);
        if (otherLayer == "Grappler") {
            return;
        }

        DamageEffect damageEffect = collision.gameObject.GetComponent<DamageEffect>();
        if (damageEffect == null) return;

        // Debug.Log("takes " + damageEffect.damage + " damage");
        health -= damageEffect.damage;
        // Debug.Log(" health: " + health);
        if (health <= 0) Die();

        invincible = true;
        invincibleDone = Time.time + invincibleDuration;

        if (damageEffect.knockback) {
            // Debug.Log("Hinox doesn't take knockback");
        }
    }

    void DecideDirection() {
        facing = Random.Range(0, 4);
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }

    public void Slam(int doSlam) {
        if (doSlam == 1) {
            damageEffect.SetDamage(6);
            damageCollider.size = new Vector2(damageCollider.size.x + damageColliderExpansion, damageCollider.size.y + damageColliderExpansion);
        } else {
            damageEffect.SetDamage(2);
            damageCollider.size = new Vector2(damageCollider.size.x + damageColliderExpansion, damageCollider.size.y + damageColliderExpansion);
        }
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

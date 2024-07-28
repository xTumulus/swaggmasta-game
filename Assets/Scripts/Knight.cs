using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(InRoom) )]
public class Knight : Enemy, IFacingMover {

    [Header("Inscribed: Knight")]
    public int speed = 2;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;

    [Header("Dynamic: Knight")]
    [Range(0,4)]
    public int facing = 0;
    public float timeNextDecision = 0;

    private InRoom inRm;
    private Animator animator;

    protected override void Awake() {
        base.Awake();
        inRm = GetComponent<InRoom>();
        animator = GetComponent<Animator>();
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
        Debug.Log("Knight hit by " + collision.gameObject.name);

        // Can't be damaged by the grappler
        string otherLayer = LayerMask.LayerToName(collision.gameObject.layer);
        Debug.Log("Other object is on layer: " + otherLayer);
        // Can't be damaged if facing swagg
        IFacingMover swaggFacingMover = collision.GetComponentInParent<IFacingMover>();
        Debug.Log("Swagg is facing " + swaggFacingMover.GetFacing());
        Debug.Log("Knight is facing " + facing);
        int oppositeSwaggFacing = (swaggFacingMover.GetFacing() + 2) % 4;

        if (otherLayer == "Grappler" || facing == oppositeSwaggFacing) {
            invincible = true;
            invincibleDone = Time.time + invincibleDuration;
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
            // Debug.Log("takes knockback");
            Vector2 delta;

            if (swaggFacingMover != null) {
                // Debug.Log("move away from Swagg");
                delta = directions[swaggFacingMover.GetFacing()];
            } else {
                // Debug.Log("move away from damage");
                delta = transform.position - collision.transform.root.position;
                if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y)) {
                    delta.x = (delta.x > 0) ? 1 : -1;
                    delta.y = 0;
                } else {
                    delta.x = 0;
                    delta.y = (delta.y > 0) ? 1 : -1;
                }
            }

            knockbackVel = delta * knockbackSpeed;
            rigid.velocity = knockbackVel;

            knockback = true;
            knockbackDone = Time.time + knockbackDuration;
            anim.speed = 0;
        }
    }

    void DecideDirection() {
        facing = Random.Range(0, 4);
        animator.Play("Knight_Walk_" + facing);
        animator.speed = 1;
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

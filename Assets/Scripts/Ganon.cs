using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganon : Enemy {

    [Header("Inscribed: Ganon")]
    public Projectile projectile;
    public int numProjectiles = 6;
    public int speed = 6;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;
    public float timeVulnerableMin = 1f;
    public float timeVulnerableMax = 2f;

    [Header("Dynamic: Ganon")]
    [Range(0,4)]
    public int facing = 0;
    public float timeNextDecision = 0;
    public float timeUntilInvincible;

    private InRoom inRm;
    private Animator animator;

    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
        inRm = GetComponent<InRoom>();
        invincible = true;
    }

    protected override void Update() {

        if (knockback) return;

        if (Time.time >= timeNextDecision) {
            DecideDirection();
        }

        rigid.velocity = directions[facing] * speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log(gameObject.name + " collided with " + collider.gameObject.name);

         // Can't be damaged by the grappler
        string otherLayer = LayerMask.LayerToName(collider.gameObject.layer);
        if (otherLayer == "Grappler") {
            return;
        }

        Debug.Log("Collision wasn't grappler");

        if (invincible) {
            Debug.Log("Ganon is invincible");
            Debug.Log("Hit by tag: " + collider.gameObject.tag);
            if (collider.gameObject.tag == "Arrow") {
                Debug.Log("Ganon was hit by an arrow");
                invincible = false;
                Debug.Log("Ganon is not invincible");
                timeUntilInvincible = Time.time + Random.Range(timeVulnerableMin, timeVulnerableMax);
                animator.Play("Ganon_Cry");
                Debug.Log("Ganon is crying");
            }
        }

        if (!invincible) {
            DamageEffect damageEffect = collider.gameObject.GetComponent<DamageEffect>();
            if (damageEffect == null) return;

            Debug.Log("Ganon takes " + damageEffect.damage + " damage");
            health -= damageEffect.damage;
            Debug.Log("Ganon health: " + health);
            if (health <= 0) Die();

            if (Time.time >= timeUntilInvincible) {
                Debug.Log("Invincibility restored. Stop crying!");
                invincible = true;
                animator.Play("Ganon_Attack");
            }
        }
    }

    void DecideDirection() {
        int[] facings = {0,0,0,1,1,1,2,2,2,3};
        facing = facings[Random.Range(0, facings.Length)];
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

    public void shootProjectile(int numProjectiles) {

        if (projectile != null) {
            for (int i = 0; i < numProjectiles; i++) {
                // Debug.Log("created projectile");
                Projectile proj = Instantiate<Projectile>(projectile, this.transform);
                proj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1, 0);
                Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 0f));
                proj.Shoot(3, direction);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    protected Vector2[] directions = new Vector2[] {
        Vector2.right, Vector2.up, Vector2.left, Vector2.down
    };

    [Header("Inscribed: Enemy")]
    public float maxHealth = 1;
    public float knockbackSpeed = 10;
    public float knockbackDuration = 0.25f;
    public float invincibleDuration = 0.5f;
    public GameObject[] randomItemDrops;
    public GameObject guaranteedItemDrop = null;

    [Header("Dynamic: Enemy")]
    public float health;
    public bool invincible = false;
    public bool knockback = false;

    // Timers
    private float invincibleDone = 0;
    private float knockbackDone = 0;

    private Vector3 knockbackVel;

    protected Animator anim;
    protected Rigidbody2D rigid;
    protected SpriteRenderer sRend;

    protected virtual void Awake() {
        health = maxHealth;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sRend = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update() {

        if (invincible && Time.time > invincibleDone) {
            invincible = false;
        }
        sRend.color = invincible ? Color.red : Color.white;

        if (knockback) {
            rigid.velocity = knockbackVel;
            if (Time.time < knockbackDone) return;
        }
        anim.speed = 1;
        knockback = false;
    }


    void OnTriggerEnter2D(Collider2D collision) {

        // Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);

        if (invincible) return;
        // Debug.Log("not invincible");

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

            IFacingMover swaggFacingMover = collision.GetComponentInParent<IFacingMover>();
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

    void Die() {

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

        Destroy(gameObject);
    }
}





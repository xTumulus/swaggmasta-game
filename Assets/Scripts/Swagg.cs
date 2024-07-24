using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(InRoom) )]
public class Swagg: MonoBehaviour, IFacingMover
{
    static public IFacingMover IFM;

    public enum eMode {
        idle,
        move,
        attack,
        roomTransition,
        knockback
    }

    [Header("Inscribed")]
    public float speed = 5;
    public float attackDuration = 0.25f;
    public float attackDelay = 0.5f;
    public float roomTransitionDelay = 0.5f;
    // public int maxHealth = 10;
    // public float knockbackSpeed = 10;
    // public float knockbackDuration = 0.25f;
    // public float invincibleDuration = 0.5f;

    [Header("Dynamic")]
    public int currentInputDirection = -1;
    public int currentCharacterDirection = 1;
    public eMode mode = eMode.idle;
    // public int numKeys = 0;
    // public bool invincible = false;
    // public bool hasGrappler = false;
    // public Vector3 lastSafeLoc;
    // public int lastSafeFacing;


    // [SerializeField]
    // private int _health;
    // public int health
    // {
    //     get { return _health; }
    //     set { _health = value; }
    // }
    //
    private float timeAtkDone = 0;
    private float timeAtkNext = 0;
    private float roomTransitionDone = 0;
    private Vector2 roomTransitionPosition;
    // private float knockbackDone = 0;
    // private float invincibleDone = 0;
    // private Vector3 knockbackVel;
    //
    // private SpriteRenderer sRend;
    private Rigidbody2D rigid;
    private Animator animator;
    private InRoom inRm;

    private Vector2[] directions = new Vector2[] {
        Vector2.right, Vector2.up, Vector2.left, Vector2.down
    };

    private KeyCode[] keys = new KeyCode[] {
        KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow,
        KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S
    };

    void Awake()
    {
        IFM = this;
        // sRend = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inRm = GetComponent<InRoom>();
        // health = maxHealth;
    }


    void Update()
    {
        // // Invincible
        // if (invincible && Time.time > invincibleDone) invincible = false;
        // sRend.color = invincible ? Color.red : Color.white;
        //
        // // Knockback
        // if (mode == eMode.knockback)
        // {
        //     rigid.velocity = knockbackVel;
        //     if (Time.time < knockbackDone) return;
        // }

        // Room Transition
        if (mode == eMode.roomTransition) {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            rigid.velocity = Vector3.zero;
            animator.speed = 0;
            posInRoom = roomTransitionPosition;
            if (Time.time < roomTransitionDone) return;

            mode = eMode.idle;
            spriteRenderer.enabled = true;
        }

        // Finish Attack
        if (mode == eMode.attack && Time.time >= timeAtkDone) {
            mode = eMode.idle;
        }

        // Handle Keyboard Input
        if (mode == eMode.move || mode == eMode.idle) {
            // Get Movement Input
            currentInputDirection = -1;
            for (int i = 0; i < keys.Length; i++) {
                if (Input.GetKey(keys[i])) {
                    currentInputDirection = i % 4;
                }
            }

            // Handle Movement Input
            if (currentInputDirection == -1) {
                mode = eMode.idle;
            } else {
                currentCharacterDirection = currentInputDirection;
                mode = eMode.move;
            }

            // Handle Attack Input
            if (Input.GetKeyDown(KeyCode.Z) && Time.time >= timeAtkNext) {
                mode = eMode.attack;
                timeAtkDone = Time.time + attackDuration;
                timeAtkNext = Time.time + attackDelay;
            }
        }

        // Act on Current Mode
        Vector2 vel = Vector2.zero;

        switch (mode)
        {
            case eMode.attack:
                animator.CrossFade("Swagg_Attack_" + currentCharacterDirection, 0);
                animator.speed = 0;
                break;
            case eMode.idle:
                animator.CrossFade("Swagg_Walk_" + currentCharacterDirection, 0);
                animator.speed = 0;
                break;
            case eMode.move:
                vel = directions[currentInputDirection];
                animator.CrossFade("Swagg_Walk_" + currentCharacterDirection, 0);
                animator.speed = 1;
                break;
        }

        rigid.velocity = vel * speed;


    }

    void LateUpdate() {

        Vector2 gridPosIR = GetGridPosInRoom(0.25f);

        int doorNum;
        for (doorNum = 0; doorNum < 4; doorNum++) {
            if (gridPosIR == InRoom.DOORS[doorNum]) {
                break;
            }
        }

        if (doorNum > 3 || doorNum != currentCharacterDirection) return;

        Vector2 rm = roomNum;
        switch (doorNum)
        {
            case 0:
                rm.x += 1;
                break;
            case 1:
                rm.y += 1;
                break;
            case 2:
                rm.x -= 1;
                break;
            case 3:
                rm.y -= 1;
                break;
        }

        if (rm.x >= 0 && rm.x <= InRoom.MAX_RM_X) {
            if (rm.y >= 0 && rm.y <= InRoom.MAX_RM_Y) {
                roomNum = rm;
                roomTransitionPosition = InRoom.DOORS[(doorNum + 2) % 4];
                posInRoom = roomTransitionPosition;
                mode = eMode.roomTransition;
                roomTransitionDone = Time.time + roomTransitionDelay;
            }
        }
    }
    //
    // void OnCollisionEnter(Collision coll)
    // {
    //     if (invincible) return;
    //     DamageEffect dEf = coll.gameObject.GetComponent<DamageEffect>();
    //     if (dEf == null) return;
    //     health -= dEf.damage;
    //     invincible = true;
    //     invincibleDone = Time.time + invincibleDuration;
    //     if (dEf.knockback)
    //     {
    //
    //         Vector3 delta = transform.position - coll.transform.position;
    //         if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
    //         {
    //
    //             delta.x = (delta.x > 0) ? 1 : -1;
    //             delta.y = 0;
    //         }
    //         else
    //         {
    //
    //             delta.x = 0;
    //             delta.y = (delta.y > 0) ? 1 : -1;
    //         }
    //
    //         knockbackVel = delta * knockbackSpeed;
    //         rigid.velocity = knockbackVel;
    //
    //         mode = eMode.knockback;
    //         knockbackDone = Time.time + knockbackDuration;
    //     }
    // }
    //
    // void OnTriggerEnter(Collider colld)
    // {
    //     PickUp pup = colld.GetComponent<PickUp>();
    //     if (pup == null) return;
    //
    //
    //     switch (pup.itemType)
    //     {
    //         case PickUp.eType.health:
    //             health = Mathf.Min(health + 2, maxHealth);
    //             break;
    //
    //
    //         case PickUp.eType.key:
    //             keyCount++;
    //             break;
    //
    //         case PickUp.eType.grappler:
    //             hasGrappler = true;
    //             break;
    //     }
    //     Destroy(colld.gameObject);
    // }
    //
    // public void ResetInRoom(int healthLoss = 0)
    // {
    //     transform.position = lastSafeLoc;
    //     currentCharacterDirection = lastSafeFacing;
    //     health -= healthLoss;
    //     invincible = true;
    //     invincibleDone = Time.time + invincibleDuration;
    // }

    public int GetFacing(){
        return currentCharacterDirection;
    }

    public float GetSpeed() {
        return speed;
    }

    public bool moving {
        get { return(mode==eMode.move); }
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
    //
    // public int keyCount
    // {
    //     get { return numKeys; }
    //     set { numKeys = value; }
    // }
}

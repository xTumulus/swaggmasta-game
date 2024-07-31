using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(InRoom) )]
public class Swagg: MonoBehaviour, IFacingMover, IKeyMaster {

    static private Swagg instance;
    static public IFacingMover IFM;

    public enum eMode {
        idle,
        move,
        attack,
        roomTransition,
        knockback,
        gadget
    }

    [Header("Inscribed")]
    public float speed = 5;
    public float attackDuration = 0.25f;
    public float attackDelay = 0.5f;
    public float roomTransitionDelay = 0.5f;
    public int maxHealth = 10;
    public float knockbackSpeed = 10;
    public float knockbackDuration = 0.25f;
    public float invincibleDuration = 0.5f;
    public int maxWallet = 99;
    public PlayerGUI gui;
    public GameObject sword;
    public NavManager navManager ;
    public RuntimeAnimatorController swaggP_Animator;

    [Header("Dynamic")]
    public int currentInputDirection = -1;
    public int currentCharacterDirection = 1;
    public eMode mode = eMode.idle;
    public bool invincible = false;
    public int healthFromPickup = 2;
    public KeyCode keyAttack = KeyCode.Z;
    public KeyCode keyGadget = KeyCode.X;
    public KeyCode keyBow = KeyCode.C;
    public KeyCode keyMenu = KeyCode.M;

    [SerializeField]
    private bool startWithGrappler = false;

    [SerializeField] [Range(0, 10)]
    private int _health;

    [SerializeField]
    private int _lootKeys;

    [SerializeField]
    private int _dungeonKeys;

    [SerializeField]
    private int _wallet;

    [SerializeField]
    private bool _hasCompass;

    [SerializeField]
    private bool _hasMap;

    [SerializeField]
    private bool _hasBossKey;

    [SerializeField]
    private bool _hasHoodie;

    [SerializeField]
    private bool _hasSword;

    [SerializeField]
    private bool _hasGrappler;

    [SerializeField]
    private bool _hasBow;

    private Vector3 lastSafeLoc;
    private int lastSafeFacing;

    // Timers
    private float timeAtkDone = 0;
    private float timeAtkNext = 0;
    private float roomTransitionDone = 0;
    private Vector2 roomTransitionPosition;
    private float knockbackDone = 0;
    private float invincibleDone = 0;

    // Effects
    private Vector3 knockbackVel;

    private SpriteRenderer sRend;
    private Rigidbody2D rigid;
    private Animator animator;
    private InRoom inRm;
    private Grappler grappler;
    private Bow bow;
    private Collider2D collider;

    private Vector2[] directions = new Vector2[] {
        Vector2.right, Vector2.up, Vector2.left, Vector2.down
    };

    private KeyCode[] keys = new KeyCode[] {
        KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow,
        KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S
    };

    void Awake() {
        instance = this;
        IFM = this;
        sRend = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health = maxHealth;

        inRm = GetComponent<InRoom>();
        bow = GetComponentInChildren<Bow>();
        grappler = GetComponentInChildren<Grappler>();
        if (startWithGrappler) currentGadget = grappler;
        collider = GetComponent<Collider2D>();

        int totalKeys = 0;
        if (PlayerPrefs.GetInt("hasLink") == 1) totalKeys++;
        if (PlayerPrefs.GetInt("hasBen") == 1) totalKeys++;
        if (PlayerPrefs.GetInt("hasError") == 1) totalKeys++;
        if (PlayerPrefs.GetInt("hasBow") == 1) totalKeys++;
        if (PlayerPrefs.GetInt("hasKey") == 1) totalKeys++;
        if (PlayerPrefs.GetInt("hasSwagg") == 1) totalKeys++;
        if (PlayerPrefs.GetInt("hasRuto") == 1) totalKeys++;

        _dungeonKeys = totalKeys;

        if (PlayerPrefs.GetInt("hasMoon") == 1) _hasBossKey = true;
    }

    void Start() {
        lastSafeLoc = transform.position;
        lastSafeFacing = currentCharacterDirection;
    }

    void Update() {

        if (Input.GetKeyDown(keyMenu)) navManager.ReturnToMenu();

        if (isControlled) return;

        // Invincible
        if (invincible && Time.time > invincibleDone) {
            invincible = false;
        }
        sRend.color = invincible ? Color.red : Color.white;

        // Knockback
        if (mode == eMode.knockback) {
            rigid.velocity = knockbackVel;
            if (Time.time < knockbackDone) return;

            mode = eMode.idle;
        }

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

            if (Input.GetKeyDown(keyGadget)) {
                if (currentGadget != null) {
                    if (currentGadget.GadgetUse(this, GadgetIsDone)) {
                        mode = eMode.gadget;
                        rigid.velocity = Vector2.zero;
                    }
                }
            }

            // Handle Attack Input
            if (Input.GetKeyDown(keyAttack) && Time.time >= timeAtkNext) {
                mode = eMode.attack;
                timeAtkDone = Time.time + attackDuration;
                timeAtkNext = Time.time + attackDelay;
            }

            if (Input.GetKeyDown(keyBow) && hasBow) {
                bow.ShootProj();
            }
        }

        // Act on Current Mode
        Vector2 vel = Vector2.zero;

        // string attackAnimName = "Swagg_Attack_";
        // string walkAnimName = "Swagg_Walk_";
        // if (_hasHoodie) {
        //     attackAnimName = "SwaggP_Attack_";
        //     walkAnimName = "SwaggP_Attack_";
        // }

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
            case eMode.gadget:
                animator.Play("Swagg_Attack_" + currentCharacterDirection);
                animator.speed = 0;
                break;
        }

        rigid.velocity = vel * speed;
    }

    void LateUpdate() {

        if (isControlled) return;

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

                lastSafeLoc = transform.position;
                lastSafeFacing = currentCharacterDirection;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        // Debug.Log("Swagg colided with " + collision.gameObject.name);
        if (isControlled) return;

        // Debug.Log("Swagg is not controlled");

        if (invincible) return;

        // Debug.Log("Swagg is not invincible");

        DamageEffect damageEffect = collision.gameObject.GetComponent<DamageEffect>();
        if (damageEffect == null) return;
        // Debug.Log ("Damage Effect for collision is not null");
        if (_hasHoodie) {
            health -= (damageEffect.damage/2);
        } else {
            health -= damageEffect.damage;
        }

        if (health <= 0) {
            navManager.GameOver();
        }

        // Debug.Log("Swagg took damage: " + damageEffect.damage);
        invincible = true;
        invincibleDone = Time.time + invincibleDuration;

        if (damageEffect.knockback) {

            Vector3 delta = transform.position - collision.transform.position;
            if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y)) {
                delta.x = (delta.x > 0) ? 1 : -1;
                delta.y = 0;
            } else {
                delta.x = 0;
                delta.y = (delta.y > 0) ? 1 : -1;
            }

            knockbackVel = delta * knockbackSpeed;
            rigid.velocity = knockbackVel;

            if (mode != eMode.gadget || currentGadget.GadgetCancel()) {
                mode = eMode.knockback;
                knockbackDone = Time.time + knockbackDuration;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (isControlled) return;
        string otherLayer = LayerMask.LayerToName(collision.gameObject.layer);
        if (otherLayer == "Default") return;

        PickUp pickup = collision.GetComponent<PickUp>();
        if (pickup == null) return;

        if (_wallet < pickup.cost) return;
        _wallet -= pickup.cost;

        switch (pickup.itemType) {
            case PickUp.eType.health:
                health = Mathf.Min(health + 2, maxHealth);
                Destroy(collision.gameObject);
                break;
            case PickUp.eType.money:
                if (wallet < maxWallet) _wallet++;
                Destroy(collision.gameObject);
                break;
            case PickUp.eType.moneyBig:
                if (wallet < maxWallet) _wallet += 5;
                Destroy(collision.gameObject);
                break;
            case PickUp.eType.key:
                _lootKeys++;
                collision.gameObject.SetActive(false);
                break;
            case PickUp.eType.compass:
                _hasCompass = true;
                collision.gameObject.SetActive(false);
                break;
            case PickUp.eType.map:
                _hasMap = true;
                collision.gameObject.SetActive(false);
                break;
            case PickUp.eType.grappler:
                _hasGrappler = true;
                currentGadget = grappler;
                collision.gameObject.SetActive(false);
                break;
            case PickUp.eType.hoodie:
                // GetComponent<Animator>().runtimeAnimatorController = swaggP_Animator;
                // animator = swaggP_Animator;
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                _hasHoodie = true;
                collision.gameObject.SetActive(false);
                break;
            case PickUp.eType.sword:
                sword.gameObject.GetComponentInChildren<DamageEffect>().damage += 1;
                _hasSword = true;
                collision.gameObject.SetActive(false);
                break;
            case PickUp.eType.bow:
                _hasBow = true;
                collision.gameObject.SetActive(false);
                break;
        }

        gui.RefreshGUI();

    }

    public void ResetInRoom(int healthLoss = 0) {
        transform.position = lastSafeLoc;
        currentCharacterDirection = lastSafeFacing;
        health -= healthLoss;

        invincible = true;
        invincibleDone = Time.time + invincibleDuration;
    }

    public int health {
        get { return _health; }
        set { _health = value; }
    }
    static public int HEALTH { get {return instance._health;} }

    public int wallet {
        get { return _wallet; }
        set { _wallet = value; }
    }
    static public int WALLET { get {return instance._wallet;} }

    // IFacingMover Implementation
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

    // IKeyMaster
    public int dungeonKeys {
        get { return _dungeonKeys; }
        set { _dungeonKeys = value; }
    }
    static public int DUNGEON_KEYS { get {return instance._dungeonKeys;} }

    public int lootKeys {
        get { return _lootKeys; }
        set { _lootKeys = value; }
    }
    static public int LOOT_KEYS { get {return instance._lootKeys;} }

    public bool hasCompass {
        get { return _hasCompass; }
    }
    static public bool HAS_COMPASS { get {return instance._hasCompass;} }

    public bool hasMap {
        get { return _hasMap; }
    }
    static public bool HAS_MAP { get {return instance._hasMap;} }

    public bool hasBossKey {
        get { return _hasBossKey; }
    }
    static public bool HAS_BOSS_KEY { get {return instance._hasBossKey;} }

    public bool hasHoodie {
        get { return _hasHoodie; }
    }
    static public bool HAS_TUNIC { get {return instance._hasHoodie;} }

    public bool hasSword {
        get { return _hasSword; }
    }
    static public bool HAS_SWORD { get {return instance._hasSword;} }

    public bool hasGrappler {
        get { return _hasGrappler; }
    }
    static public bool HAS_GRAPPLER { get {return instance._hasGrappler;} }

    public bool hasBow {
        get { return _hasBow; }
    }
    static public bool HAS_BOW { get {return instance._hasBow;} }

    public Vector2 position {
        get { return (Vector2) transform.position; }
    }

    // IGadget
    #region IGadget_Affordances
    public IGadget currentGadget { get; private set; }

    public bool GadgetIsDone(IGadget gadget) {
        if (gadget != currentGadget) {
            Debug.LogError("non-current gadget called GadgetDone");
        }

        controlledBy = null;
        physicsEnabled = true;
        mode = eMode.idle;
        return true;
    }

    public IGadget controlledBy { get; set; }
    public bool isControlled {
        get { return (controlledBy != null); }
    }

    private bool _physicsEnabled = true;
    public bool physicsEnabled {
        get { return _physicsEnabled; }
        set {
            if (_physicsEnabled != value) {
                _physicsEnabled = value;
                collider.enabled = _physicsEnabled;
                rigid.simulated = _physicsEnabled;
            }
        }
    }
    #endregion
}

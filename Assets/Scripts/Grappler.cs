using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(LineRenderer) )]
public class Grappler : MonoBehaviour, IGadget {

    public enum eMode {
      gIdle,
      gOut,
      gRetract,
      gPull
    }

    [Header("Inscribed")]
    public float grappleSpd = 10;
    public float maxLength = 7.25f;
    public float minLength = 0.375f;

    [Header("Dynamic")] [SerializeField]
    private eMode _mode = eMode.gIdle;
    public eMode mode {
      get { return _mode; }
      private set { _mode = value; }
    }

    private LineRenderer line;
    public Rigidbody2D rigid;
    private Collider2D collider;

    private Vector3 p0, p1;
    private int swaggDirection;
    private Swagg swagg;
    private System.Func<IGadget, bool> gadgetDoneCallback;

    private Vector2[] directions = new Vector2[] {
      Vector2.right, Vector2.up, Vector2.left, Vector2.down
    };

    private Vector3[] dirV3s = new Vector3[] {
      Vector3.right, Vector3.up, Vector3.left, Vector3.down
    };

    void Awake() {
      line = GetComponent<LineRenderer>();
      rigid = GetComponent<Rigidbody2D>();
      collider = GetComponent<Collider2D>();
    }

    void Start() {
      gameObject.SetActive(false);
    }

    void SetGrappleMode(eMode newMode) {
      switch (newMode) {
        case eMode.gIdle:
          transform.DetachChildren();
          gameObject.SetActive(false);
          break;
        case eMode.gOut:
          gameObject.SetActive(true);
          rigid.velocity = directions[swaggDirection] * grappleSpd;
          break;
        case eMode.gRetract:
          gameObject.SetActive(true);
          rigid.velocity = -directions[swaggDirection] * (grappleSpd * 2);
          break;
        case eMode.gPull:
          break;
      }

      mode = newMode;
    }

    void FixedUpdate() {
      p1 = transform.position;
      line.SetPosition(1, p1);

      switch(mode) {
        case eMode.gOut:
          if ((p1 - p0).magnitude >= maxLength) {
            SetGrappleMode(eMode.gRetract);
          }
          break;
        case eMode.gRetract:
          if (Vector3.Dot((p1 - p0), dirV3s[swaggDirection] ) < 0) {
            GrappleDone();
          }
          break;
        case eMode.gPull:
          break;
      }
    }

    void LateUpdate() {
      p1 = transform.position;
      line.SetPosition(1, p1);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      string otherLayer = LayerMask.LayerToName(collider.gameObject.layer);

      switch(otherLayer) {
        case "Items":
          PickUp pickup = collider.GetComponent<PickUp>();
          if (pickup == null) return;

          pickup.transform.SetParent(transform);
          pickup.transform.localPosition = Vector3.zero;
          SetGrappleMode(eMode.gRetract);
          break;
        case "Enemies":
          Enemy e = collider.GetComponent<Enemy>();
          if (e != null) SetGrappleMode(eMode.gRetract);
          break;
        case "GrapTiles":
          SetGrappleMode(eMode.gPull);
          break;
        default :
          SetGrappleMode(eMode.gRetract);
          break;
      }
    }

    void GrappleDone() {
      SetGrappleMode( eMode.gIdle);
      gadgetDoneCallback(this);
    }

    // IGadget
    public bool GadgetUse(Swagg tempSwagg, System.Func<IGadget, bool> tempCallback) {
      if (mode != eMode.gIdle) return false;

      swagg = tempSwagg;
      gadgetDoneCallback = tempCallback;
      transform.localPosition = Vector3.zero;

      swaggDirection = swagg.GetFacing();
      p0 = swagg.transform.position;
      p1 = p0 + (dirV3s[swaggDirection] * minLength);
      gameObject.transform.position = p1;
      gameObject.transform.rotation = Quaternion.Euler(0, 0, 90 * swaggDirection);

      line.positionCount = 2;
      line.SetPosition(0, p0);
      line.SetPosition(1, p1);

      SetGrappleMode(eMode.gOut);

      return true;
    }

    public bool GadgetCancel() {
      if (mode == eMode.gPull) {
        return false;
      }

      SetGrappleMode(eMode.gIdle);
      gameObject.SetActive(false);
      return true;
    }
}

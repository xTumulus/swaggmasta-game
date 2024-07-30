using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    // public enum eMode {
    //   gIdle,
    //   gShoot
    // }

    [Header("Inscribed")]
    public Projectile arrow;
    public float timeBetweenShots = 1;

    [Header("Dynamic")] [SerializeField]
    // private eMode _mode = eMode.gIdle;
    // public eMode mode {
    //   get { return _mode; }
    //   private set { _mode = value; }
    // }
    public float timeToNextShot = 0;

    private Vector3 p0, p1;
    private int swaggDirection;
    private Swagg swagg;
    // private System.Func<IGadget, bool> gadgetDoneCallback;

    private Vector2[] directions = new Vector2[] {
      Vector2.right, Vector2.up, Vector2.left, Vector2.down
    };

    private Vector3[] dirV3s = new Vector3[] {
      Vector3.right, Vector3.up, Vector3.left, Vector3.down
    };

    public void Awake() {
      swagg = GetComponentInParent<Swagg>();
    }

    public void ShootProj() {
        // Debug.Log("Shooting bow");

        if (Time.time < timeToNextShot) return;

        // Debug.Log("Shot timer is up");

        if (arrow != null && swagg.wallet > 0) {
          // Debug.Log("arrow and wallet are valid");
            Projectile proj = Instantiate<Projectile>(arrow);
            // Debug.Log("created bow projectile");

            proj.transform.position = this.transform.position;

            swaggDirection = swagg.GetFacing();
            proj.transform.rotation = Quaternion.Euler(0, 0, 90 * swaggDirection);

            proj.Shoot(swaggDirection, directions[swaggDirection]);

            timeToNextShot = Time.time + timeBetweenShots;
            swagg.wallet--;
        }

        // SetBowMode(eMode.gIdle);
        // gadgetDoneCallback(this);
    }

    // void SetBowMode(eMode newMode) {
    //
    //     switch (newMode) {
    //         case eMode.gIdle:
    //             transform.DetachChildren();
    //             gameObject.SetActive(false);
    //
    //             Debug.Log("Changing gadget mode to idle");
    //             Debug.Log("Swagg: " + swagg);
    //             Debug.Log("controlledBy: " + swagg.controlledBy);
    //             // if(swagg != null && swagg.controlledBy == this as IGadget) {
    //                 swagg.controlledBy = null;
    //                 swagg.physicsEnabled = true;
    //             // }
    //             Debug.Log("controlledBy: " + swagg.controlledBy);
    //             break;
    //         case eMode.gShoot:
    //             gameObject.SetActive(true);
    //             // swagg.controlledBy = this;
    //             // swagg.physicsEnabled = false;
    //             swagg.controlledBy = null;
    //             swagg.physicsEnabled = true;
    //             Debug.Log("Changing gadget mode to shoot");
    //
    //             ShootProj();
    //             break;
    //     }
    //
    //   mode = newMode;
    //   Debug.Log("Set mode to " + newMode + " at end of SetBowMode");
    // }

    // // IGadget
    // public bool GadgetUse(Swagg tempSwagg, System.Func<IGadget, bool> tempCallback) {
    //     Debug.Log("Using gadget");
    //   if (mode != eMode.gIdle) return false;
    //
    //   swagg = tempSwagg;
    //   gadgetDoneCallback = tempCallback;
    //   transform.localPosition = Vector3.zero;
    //
    //
    //   SetBowMode(eMode.gShoot);
    //
    //   return true;
    // }
    //
    // public bool GadgetCancel() {
    //
    //   SetBowMode(eMode.gIdle);
    //   gameObject.SetActive(false);
    //   return true;
    // }
}

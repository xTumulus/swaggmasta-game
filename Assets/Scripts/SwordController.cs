using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [Header("Inscribed")]
    public Sprite[] swordSprites;

    private GameObject sword;
    private Swagg swagg;
    Vector3 swaggPosition;

    void Start()
    {
        sword = transform.Find("Sword").gameObject;
        swagg = GetComponentInParent<Swagg>();

        // Deactivate the sword
        sword.SetActive(false);
    }


    void Update()
    {
        // Select correct Sprite
        SpriteRenderer swordSpriteRenderer = sword.GetComponent<SpriteRenderer>();
        swordSpriteRenderer.sprite = swordSprites[swagg.currentCharacterDirection];

        //Place Sprite in front of Swagg
        swaggPosition = swagg.transform.position;
        switch (swagg.currentCharacterDirection) {
            case -1:
                sword.transform.position = swaggPosition;
                break;
            case 0:
                sword.transform.position = new Vector3(swaggPosition.x + 0.95f, swaggPosition.y, swaggPosition.z);
                break;
            case 1:
                sword.transform.position = new Vector3(swaggPosition.x, swaggPosition.y + 0.95f, swaggPosition.z);
                break;
            case 2:
                sword.transform.position = new Vector3(swaggPosition.x + -0.95f, swaggPosition.y, swaggPosition.z);
                break;
            case 3:
                sword.transform.position = new Vector3(swaggPosition.x, swaggPosition.y - 0.95f, swaggPosition.z);
                break;
        }

        sword.SetActive(swagg.mode == Swagg.eMode.attack);
    }

}

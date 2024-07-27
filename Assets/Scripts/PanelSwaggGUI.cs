using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwaggGUI : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Sprite healthEmpty;
    public Sprite healthHalf;
    public Sprite healthFull;
    public Sprite bossKey;
    public Sprite map;
    public Sprite compass;
    public Sprite hoodie;
    public Sprite shield;
    public Sprite sword;
    public Sprite grappler;

    Text dungeonKeyText;
    Text lootKeyText;
    Text walletText;
    Image bossKeyIcon;
    List<Image> healthImages;

    void Start() {

        Transform tempTransform;

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Countable");
        tempTransform = tempTransform.Find("KeysDungeon");
        tempTransform = tempTransform.Find("KeyCount");
        dungeonKeyText = tempTransform.GetComponent<Text>();

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Countable");
        tempTransform = tempTransform.Find("KeysLoot");
        tempTransform = tempTransform.Find("KeyCount");
        lootKeyText = tempTransform.GetComponent<Text>();

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Countable");
        tempTransform = tempTransform.Find("Wallet");
        tempTransform = tempTransform.Find("WalletCount");
        walletText = tempTransform.GetComponent<Text>();

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Dungeon");
        tempTransform = tempTransform.Find("IconBossKey");
        bossKeyIcon = tempTransform.GetComponent<Image>();

        Transform healthPanel = transform.Find("Health");
        healthImages = new List<Image>();
        if (healthPanel != null) {
            for (int i = 0; i < 5; i++)
            {
                tempTransform = healthPanel.Find("H_" + i);
                if ( tempTransform == null) break;
                healthImages.Add(tempTransform.GetComponent<Image>());
            }
        }

        if (Swagg.HAS_BOSS_KEY) {
            bossKeyIcon.GetComponent<Image>().sprite = bossKey;
        }
    }

    void Update() {

        lootKeyText.text = Swagg.LOOT_KEYS.ToString();
        dungeonKeyText.text = Swagg.DUNGEON_KEYS.ToString();
        walletText.text = Swagg.WALLET.ToString();

        int health = Swagg.HEALTH;
        for (int i = 0; i < healthImages.Count; i++) {
            if (health > 1) {
                healthImages[i].sprite = healthFull;
            } else if (health == 1) {
                healthImages[i].sprite = healthHalf;
            } else {
                healthImages[i].sprite = healthEmpty;
            }

            health -= 2;
        }
    }

    // TODO have it only update keys on changes instead of every update
    // public changeKeyValues() {}
}

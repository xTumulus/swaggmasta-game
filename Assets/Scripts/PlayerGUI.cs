using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Sprite healthEmpty;
    public Sprite healthHalf;
    public Sprite healthFull;
    public Sprite bossKey;
    public Sprite map;
    public Sprite compass;
    public Sprite hoodie;
    public Sprite sword;
    public Sprite grappler;
    public Sprite bow;

    List<Image> healthImages;
    Text dungeonKeyText;
    Text lootKeyText;
    Text walletText;
    Image compassIcon;
    Image mapIcon;
    Image bossKeyIcon;
    Image tunicIcon;
    Image swordIcon;
    Image grapplerIcon;
    Image bowIcon;

    void Start() {

        PullElementReferences();
        InitialHealthSetup();
        RefreshGUI();
    }

    private void PullElementReferences() {
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
        tempTransform = tempTransform.Find("IconCompass");
        compassIcon = tempTransform.GetComponent<Image>();

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Dungeon");
        tempTransform = tempTransform.Find("IconMap");
        mapIcon = tempTransform.GetComponent<Image>();

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Dungeon");
        tempTransform = tempTransform.Find("IconBossKey");
        bossKeyIcon = tempTransform.GetComponent<Image>();

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Upgrades");
        tempTransform = tempTransform.Find("IconTunic");
        tunicIcon = tempTransform.GetComponent<Image>();

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Upgrades");
        tempTransform = tempTransform.Find("IconSword");
        swordIcon = tempTransform.GetComponent<Image>();

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Equipment");
        tempTransform = tempTransform.Find("IconGrappler");
        grapplerIcon = tempTransform.GetComponent<Image>();

        tempTransform = transform.Find("SwaggInventory");
        tempTransform = tempTransform.Find("Equipment");
        tempTransform = tempTransform.Find("IconBow");
        bowIcon = tempTransform.GetComponent<Image>();
    }

    private void InitialHealthSetup() {

        Transform tempTransform;

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
    }

    public void RefreshGUI() {
        lootKeyText.text = Swagg.LOOT_KEYS.ToString();
        dungeonKeyText.text = Swagg.DUNGEON_KEYS.ToString();
        walletText.text = Swagg.WALLET.ToString();

        if (Swagg.HAS_COMPASS) { compassIcon.GetComponent<Image>().sprite = compass; }
        if (Swagg.HAS_MAP) { mapIcon.GetComponent<Image>().sprite = map; }
        if (Swagg.HAS_BOSS_KEY) { bossKeyIcon.GetComponent<Image>().sprite = bossKey; }
        if (Swagg.HAS_TUNIC) { tunicIcon.GetComponent<Image>().sprite = hoodie; }
        if (Swagg.HAS_SWORD) { swordIcon.GetComponent<Image>().sprite = sword; }
        if (Swagg.HAS_GRAPPLER) { grapplerIcon.GetComponent<Image>().sprite = grappler; }
        if (Swagg.HAS_BOW) { bowIcon.GetComponent<Image>().sprite = bow; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            this.OpenInventory();
        }
    }

    public void OpenInventory() {
        SceneManager.LoadScene("Inventory");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzle : MonoBehaviour {

    [Header("Inscribed")]
    public GameObject prize;
    public PushBlock[] blocks;

    void Awake() {
        prize.SetActive(false);
    }

    void Update() {
        foreach (PushBlock block in blocks) {
            if (block.inPlace != true) return;
        }

        Debug.Log(gameObject.name + " has all blocks in place");
        // Play sound?
        prize.SetActive(true);
    }
}

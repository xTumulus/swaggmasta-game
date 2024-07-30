using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class KeyChecker : MonoBehaviour {

    [SerializeField]
    public TMP_InputField inputText;
    public TMP_Text resultText;

    public void CheckKey() {
        Debug.Log(inputText.text);

        bool success = false;

        if (String.Equals(inputText.text, "Link")) {
            PlayerPrefs.SetInt("hasLink", 1);
            success = true;
        } else if (String.Equals(inputText.text, "Ben")) {
            PlayerPrefs.SetInt("hasBen", 1);
            success = true;
        } else if (String.Equals(inputText.text, "Error")) {
            PlayerPrefs.SetInt("hasError", 1);
            success = true;
        } else if (String.Equals(inputText.text, "Bow")) {
            PlayerPrefs.SetInt("hasBow", 1);
            success = true;
        } else if (String.Equals(inputText.text, "Key")) {
            PlayerPrefs.SetInt("hasKey", 1);
            success = true;
        } else if (String.Equals(inputText.text, "Swagg")) {
            PlayerPrefs.SetInt("hasSwagg", 1);
            success = true;
        } else if (String.Equals(inputText.text, "Ruto")) {
            PlayerPrefs.SetInt("hasRuto", 1);
            success = true;
        } else if (String.Equals(inputText.text, "Moon")) {
            PlayerPrefs.SetInt("hasMoon", 1);
            success = true;
        }

        if (success) {
            resultText.text = "Success!";
        } else {
            resultText.text = "Invalid Key";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavManager : MonoBehaviour
{
    public void ReturnToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void EnterKey() {
        SceneManager.LoadScene("KeyEntry");
    }
}

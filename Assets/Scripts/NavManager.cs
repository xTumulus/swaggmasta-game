using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavManager : MonoBehaviour
{
    public void ReturnToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void EnterKey() {
        SceneManager.LoadScene("KeyEntry");
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
}

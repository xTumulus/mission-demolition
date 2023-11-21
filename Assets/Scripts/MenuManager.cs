using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public int difficulty = 0;

    public void setDifficulty(TMP_Dropdown dropdown) {
      this.difficulty = dropdown.value;
    }

    public void StartGame() {
      switch (difficulty) {
        case 0:
          SceneManager.LoadScene("Easy");
          break;
        case 1:
          SceneManager.LoadScene("Medium");
          break;
        case 2:
          SceneManager.LoadScene("Hard");
          break;
      }
    }

    public void QuitGame() {
      Application.Quit();
    }
}

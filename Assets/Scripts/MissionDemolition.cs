using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode {
  idle,
  playing,
  levelEnd
}

public class MissionDemolition : MonoBehaviour
{
  static private MissionDemolition gameSingleton;

  [Header("Inscribed")]
  public Text levelUI;
  public Text shotsUI;
  public Text successUI;
  public Text endGameUI;
  public Vector3 castlePosition;
  public GameObject[] castles;

  [Header("Dynamic")]
  public int level;
  public int levelMax;
  public int shotsTaken;
  public GameObject castle;
  public GameMode mode = GameMode.idle;
  public string showing = "Show Slingshot";

  void Start() {
    gameSingleton = this;

    level = 0;
    shotsTaken = 0;
    levelMax = castles.Length;
    successUI.gameObject.SetActive(false);

    StartLevel();
  }

  private void StartLevel() {
    if (castle != null) {
      Destroy(castle);
    }

    Projectile.DESTROY_PROJECTILES();

    castle = Instantiate<GameObject>(castles[level]);
    castle.transform.position = castlePosition;

    Goal.goalMet = false;

    UpdateGUI();

    mode = GameMode.playing;
  }

  private void UpdateGUI() {
    levelUI.text = "Level: " + (level + 1) + " of " + levelMax;
    shotsUI.text = "Shots Taken: " + shotsTaken;
  }

  void Update() {
    UpdateGUI();

    if ((mode == GameMode.playing) && Goal.goalMet) {
      mode = GameMode.levelEnd;

      Invoke("NextLevel", 2f);
    }
  }

  private void NextLevel() {
    level++;

    if (level == levelMax) {
      level = 0;
      shotsTaken = 0;
      StartCoroutine("ShowEndGame");
      SceneManager.LoadScene("StartScreen");
    }

    StartCoroutine("ShowSuccess");    
    StartLevel();
  }

  private IEnumerator ShowSuccess() {
    successUI.gameObject.SetActive(true);

    yield return new WaitForSeconds(3);
    
    successUI.gameObject.SetActive(false);
  }

  private IEnumerator ShowEndGame() {
    successUI.gameObject.SetActive(true);
    endGameUI.text = "Difficulty Completed with " + shotsTaken + " shots!";
    // endGameUI.gameObject.SetActive(true);

    yield return new WaitForSeconds(5);

    successUI.gameObject.SetActive(false);
  }

  static public void SHOT_FIRED() {
    gameSingleton.shotsTaken++;
  }

  static public GameObject GET_CASTLE() {
    return gameSingleton.castle;
  }
}

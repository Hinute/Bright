using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleEndLevel : MonoBehaviour {

    public GameObject deathScreen;
    public GameObject winScreen;
    public Text maxSize;
    private bool updated = false;
    // Start is called before the first frame update
    void Start() {
        winScreen.SetActive(false);
        deathScreen.SetActive(false);
        Player.isDead = false;
        updated = false;
        Debug.Log("HandleDeath: Started");
    }

    // Update is called once per frame
    void Update() {
        if (Player.isDead && !updated) {
            killPlayer();
        } else if (LevelCompletion.levelCompleted && !updated) {
            showWinScreen();
        }
    }

    public void ReturnToMainMenu() {
        Player.isWon = false;
        deathScreen.SetActive(false);
        AudioManager.instance.PlaySound("Select");
        SceneManager.LoadScene("Bright-MainMenu");
    }

    public void Replay() {
        deathScreen.SetActive(false);
        AudioManager.instance.PlaySound("Select");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void killPlayer() {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlaySound("Die");

        deathScreen.SetActive(true);
        maxSize.text += " " + PlayerPrefs.GetInt("MaxSize", 100);
        updated = true;
    }

    void showWinScreen(){
        Debug.Log("SHOW WIN SCREEN");
        Time.timeScale = 0;
        winScreen.SetActive(true);
        updated = true;
        
    }

    public void nextLevel(){
        Player.isWon = false;
        //updated = false;
        winScreen.SetActive(false);
        LevelCompletion.instance.resetLevelCompleted();
        SceneManager.LoadScene("Em2");
    }

}
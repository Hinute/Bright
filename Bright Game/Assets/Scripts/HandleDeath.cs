using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleDeath : MonoBehaviour {

    public GameObject deathScreen;
    public Text maxSize;
    private bool updated = false;
    // Start is called before the first frame update
    void Start() {
        deathScreen.SetActive(false);
        Player.isDead = false;
        updated = false;
        Debug.Log("HandleDeath: Started");
    }

    // Update is called once per frame
    void Update() {
        if (Player.isDead && !updated) {
            killPlayer();
        }
    }

    public void ReturnToMainMenu() {
        deathScreen.SetActive(false);
        SceneManager.LoadScene("Bright-MainMenu");
    }

    void killPlayer() {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlaySound("Die");

        deathScreen.SetActive(true);
        maxSize.text += " " + PlayerPrefs.GetInt("MaxSize", 100);
        updated = true;
    }

}
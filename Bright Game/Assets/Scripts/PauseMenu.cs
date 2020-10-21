using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject pauseMenu;

    // Start is called before the first frame update

    void Awake() {

    }
    void Start() {
        pauseMenu.SetActive(false);
        SetPanelTransparency();
        isPaused = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("Escape pressed");
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
            Debug.Log("isPaused: " + isPaused);
        }
    }

    void SetPanelTransparency(float transparencyValue = .9f) {
        var panelImage = pauseMenu.GetComponent<Image>();
        var tempColor = panelImage.color;
        tempColor.a = transparencyValue;
        panelImage.color = tempColor;
    }

    void Resume() {
        AudioManager.instance.PlaySound("Select");
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause() {
        AudioManager.instance.PlaySound("Select");
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnToMainMenu() {
        AudioManager.instance.PlaySound("Select");
        SceneManager.LoadScene("Bright-MainMenu");
    }

}
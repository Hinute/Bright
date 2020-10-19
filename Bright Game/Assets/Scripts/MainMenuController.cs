using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject optionsMenu;

    void Start() {
        Debug.Log("MainMenuController: Started");
        AudioManager.instance.PlayMusic("Upbeat");
    }

    void Update() {

    }

    public void PlayGame() {
        // Loads the next scene in the build settings after this one
        // TODO: currently set to go to Will'sSandbox, need to change to the proper game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToOptionsMenu() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ToMainMenu() {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame() {
        Debug.Log("QUIT: Bye bye!");
        Application.Quit();
    }
}
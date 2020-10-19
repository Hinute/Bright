using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void PlayGame() {
        // Loads the next scene in the build settings after this one
        // TODO: currently set to go to Will'sSandbox, need to change to the proper game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToOptionsMenu() {

    }

    public void ToMainMenu() {

    }

    public void ExitGame() { }
}
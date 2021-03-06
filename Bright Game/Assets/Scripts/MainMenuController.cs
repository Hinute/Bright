﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    // TODO: @daria @emily: Need to add Bright game logo to the top of the menu
    // ideally, we'll want it to animate in and the buttons to fade in

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject instructionsMenu;

    void Start() {
        Debug.Log("MainMenuController: Started");
        AudioManager.instance.PlayMusic("Crystals");
    }

    void Update() {

    }

    public void PlayGame() {
        // Loads the next scene in the build settings after this one
        AudioManager.instance.PlaySound("Select");
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToOptionsMenu() {
        AudioManager.instance.PlaySound("Select");
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ToMainMenu() {
        AudioManager.instance.PlaySound("Select");
        optionsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ToInstructionsMenu() {
        AudioManager.instance.PlaySound("Select");
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void ExitGame() {
        Debug.Log("QUIT: Bye bye!");
        AudioManager.instance.PlaySound("Die");
        Application.Quit();
    }
}
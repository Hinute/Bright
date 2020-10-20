using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    void Start() {
        Time.timeScale = 1f;
        Debug.Log("LevelController: Started");
        AudioManager.instance.PlayMusic("Upbeat");
    }
}
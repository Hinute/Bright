using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Debug.Log("LevelController: Started");
        AudioManager.instance.PlayMusic("Upbeat");
    }

    // Update is called once per frame
    void Update() {

    }
}
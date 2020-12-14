using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletion : MonoBehaviour {

    public static bool levelCompleted = false;

    public static LevelCompletion instance;
    public Slider levelCompletionSlider;
    // Start is called before the first frame update

    private void Awake() {
        if (instance == null) { // if the instance var is null this is first AudioManager
            instance = this;
        } else {
            Destroy(gameObject); // this isnt the first so destroy it
            return; // since this isn't the first return so no other code is run
        }
    }

    void Start() {
        levelCompleted = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public void resetLevelCompleted() {
        levelCompleted = false;
    }

    public void setLevelCompletion(float percentage) {
        levelCompletionSlider.value = percentage;
        if (percentage >= 1f) {
            levelCompleted = true;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour {

    public static float maxSpeed = 3f;
    private float baseMaxSpeed = 3f;
    public static float speed = 1f;
    public static Player player;
    public Light2D playerLight;
    private float lightDecreaseSpeed = .0001f;
    private float newTargetLightRadius;
    public static bool isDead = false;
    private float baseDecreaseSpeed = .0005f;

    void Awake() {
        PlayerPrefs.SetInt("MaxSize", 0);
        if (player == null) {
            player = this;
            playerLight = this.GetComponentInChildren<Light2D>();
        }
    }

    // Update is called once per frame
    void Update() {
        if (!PauseMenu.isPaused && !isDead) {
            float currentLightRadius = playerLight.pointLightOuterRadius;
            int currentScore = (int)(currentLightRadius * 100);
            maybeSaveNewMaxScore(currentScore);
            checkMovement();
            maybeIncreasePlayerLight(currentLightRadius);
            decreasePlayerLight();
        }
    }

    /*
     * Checks if current score is higher than previous MaxScore, saves current score if true
     */
    void maybeSaveNewMaxScore(int currentScore) {
        if ((currentScore) > PlayerPrefs.GetInt("MaxSize", 0)) {
            PlayerPrefs.SetInt("MaxSize", currentScore);
            Debug.Log("Saved new score");
        }
    }

    /*
     * Checks if the player should move, and gives it some movement if it should, with some acceleration
     * at the beginning. If player should stop moving, resets the speed so acceleration can start fresh.
     */
    void checkMovement() {
        var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position += (Vector3)move * speed * Time.deltaTime;

        if (move != Vector2.zero && speed < maxSpeed) {
            speed += .01f;
        } else if (move == Vector2.zero) {
            speed = 1;
        }

        if (speed > maxSpeed) {
            speed -= .01f;
        }
    }

    /*
     * Checks if player light radius should be increasing by comparing current light radius with target light radius
     * Once current light radius is greater than or equal to target light radius, reset newTargetLightRadius to 0
     * to prevent the light from continuing to try to increase
     */
    void maybeIncreasePlayerLight(float currentLightRadius) {
        if (currentLightRadius < newTargetLightRadius) {
            playerLight.pointLightOuterRadius = Mathf.Lerp(currentLightRadius, currentLightRadius + newTargetLightRadius, .001f);
        } else {
            newTargetLightRadius = 0f;
        }
    }

    void decreasePlayerLight() {
        float lightRadius = playerLight.pointLightOuterRadius;
        if (lightRadius < .3f) {
            maxSpeed = 3.5f;
            lightDecreaseSpeed = baseDecreaseSpeed;
        } else if (lightRadius < .5f) {
            lightDecreaseSpeed = baseDecreaseSpeed * .9f;
        } else if (lightRadius <= 1f) {
            lightDecreaseSpeed = baseDecreaseSpeed * .4f;
            maxSpeed = 3f;
        } else if (lightRadius > 1f && lightRadius <= 3f) {
            lightDecreaseSpeed = baseDecreaseSpeed * lightRadius;
            maxSpeed = baseMaxSpeed - ((float)Math.Sqrt(lightRadius)/2);
        }
        else {
            lightDecreaseSpeed = baseDecreaseSpeed * 4f;
        }
        playerLight.pointLightOuterRadius -= lightDecreaseSpeed;
        if (playerLight.pointLightOuterRadius <= 0) {
            setDeathFlag();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        string gameObjectName = other.gameObject.ToString();
        Debug.Log("TRIGGERED Player: " + gameObjectName);

        if (gameObjectName.Contains("Food")) {
            eatFood(other);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject gameObjectObstruction = collision.gameObject;
        Debug.Log("NEW COLLIDED Player: " + gameObjectObstruction.ToString());

        if (gameObjectObstruction.tag == "World") {
            hitWall();
        }
    }

    void OnCollisionStay2D(Collision2D collision) {
        GameObject gameObjectObstruction = collision.gameObject;
        Debug.Log("STAYED COLLIDED Player: " + gameObjectObstruction.ToString());

        if (gameObjectObstruction.tag == "World") {
            hitWall();
        }
    }

    void setDeathFlag() {
        Debug.Log("DEATH");
        isDead = true;
    }

    void eatFood(Collider2D foodCollider) {
        Debug.Log("NOMS");

        Light2D foodLight = foodCollider.gameObject.GetComponentInChildren<Light2D>();
        float foodLightRadius = foodLight.pointLightOuterRadius;

        AudioManager.instance.PlaySound("Eat");

        if (newTargetLightRadius == 0) {
            newTargetLightRadius = playerLight.pointLightOuterRadius + foodLightRadius / 5;
        } else {
            newTargetLightRadius = newTargetLightRadius + foodLightRadius / 5;
        }
        changePlayerColor(foodLight.color);
        FoodController.instance.DestroyObject(foodCollider.gameObject);
    }

    void changePlayerColor(Color32 colorRgb) {
        playerLight.color = colorRgb;
        gameObject.GetComponent<SpriteRenderer>().color = colorRgb;
    }

    void hitWall() {
        Debug.Log("BUMP! I hit a wall!");
        AudioManager audioManager = AudioManager.instance;
        Sound[] sounds = audioManager.sounds;
        Sound wallSound = audioManager.FindAudioByName("Wall", sounds);

        if (!wallSound.source.isPlaying) {
            audioManager.PlaySound("Wall");
        }
    }
}
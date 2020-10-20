using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour {

    public static float maxSpeed = 2f;
    public static float speed = 1f;
    public static Player player;
    public Light2D playerLight;
    private float lightDecreaseSpeed = .0001f;
    private float newTargetLightRadius;

    void Awake() {
        if (player == null) {
            player = this;
            playerLight = this.GetComponentInChildren<Light2D>();
        }
    }

    void Start() { }

    // Update is called once per frame
    void Update() {
        checkMovement();
        if (playerLight.pointLightOuterRadius < newTargetLightRadius) {
            playerLight.pointLightOuterRadius = Mathf.Lerp(playerLight.pointLightOuterRadius, playerLight.pointLightOuterRadius + newTargetLightRadius, .001f);
        } else {
            newTargetLightRadius = 0f;
        }
        decreasePlayerLight();
        Debug.Log("Player light radius: " + playerLight.pointLightOuterRadius);
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

    void decreasePlayerLight() {
        float lightRadius = playerLight.pointLightOuterRadius;
        if (lightRadius <= 1f) {
            lightDecreaseSpeed = .0001f;
            maxSpeed = 4f;
        } else if (lightRadius <= 1.3f) {
            lightDecreaseSpeed = .0002f;
            maxSpeed = 3.5f;
        } else if (lightRadius <= 1.7f) {
            lightDecreaseSpeed = .0003f;
            maxSpeed = 3f;
        } else if (lightRadius <= 2f) {
            lightDecreaseSpeed = .0005f;
            maxSpeed = 2.5f;
        } else if (lightRadius <= 2.5f) {
            lightDecreaseSpeed = .0008f;
        } else if (lightRadius <= 3f) {
            lightDecreaseSpeed = .001f;
        } else if (lightRadius <= 3.5f) {
            lightDecreaseSpeed = .0012f;
        }
        playerLight.pointLightOuterRadius -= lightDecreaseSpeed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("collision");
        Debug.Log(other.gameObject.ToString());
        if (other.gameObject.ToString().Contains("Food")) {
            float foodLightRadius = other.gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius;
            Debug.Log("Success");
            if (newTargetLightRadius == 0) {
                newTargetLightRadius = playerLight.pointLightOuterRadius + foodLightRadius / 5;
            } else {
                newTargetLightRadius = newTargetLightRadius + foodLightRadius / 5;
            }
        }
        other.gameObject.SetActive(false);
        Destroy(other.gameObject);
        //FoodController.instance.SpawnFood();

    }

}
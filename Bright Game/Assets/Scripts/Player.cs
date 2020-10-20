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
        if (!PauseMenu.isPaused) {
            checkMovement();
            if (playerLight.pointLightOuterRadius < newTargetLightRadius) {
                playerLight.pointLightOuterRadius = Mathf.Lerp(playerLight.pointLightOuterRadius, playerLight.pointLightOuterRadius + newTargetLightRadius, .001f);
            } else {
                newTargetLightRadius = 0f;
            }
            decreasePlayerLight();
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
        Debug.Log("TRIGGERED Player: " + other.gameObject.ToString());

        if (other.gameObject.ToString().Contains("Food")) {
            float foodLightRadius = other.gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius;
            if (newTargetLightRadius == 0) {
                newTargetLightRadius = playerLight.pointLightOuterRadius + foodLightRadius / 5;
            } else {
                newTargetLightRadius = newTargetLightRadius + foodLightRadius / 5;
            }
            FoodController.instance.DestroyObject(other.gameObject);
        }

        if (other.gameObject.tag == "World") {
            Debug.Log("BUMP! I hit a wall!");

            // TODO: need to add wall bump sound
            // AudioManager.instance.PlaySound("Thud");

            var xPosition = transform.position.x;
            var yPosition = transform.position.y;
            // Any lower and player can eventually go through the wall
            var wallBufferDistance = .3f;

            // Bounce off the wall a bit to prevent you from going through
            if (xPosition >= 0) {
                xPosition -= wallBufferDistance;
            } else {
                xPosition += wallBufferDistance;
            }
            if (yPosition >= 0) {
                yPosition -= wallBufferDistance;
            } else {
                yPosition += wallBufferDistance;
            }
            transform.position = (Vector3)(new Vector2(xPosition, yPosition));
        }

    }

}

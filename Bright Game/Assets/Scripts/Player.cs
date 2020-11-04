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
    public static bool isDead = false;

    void Awake() {
        PlayerPrefs.SetInt("MaxSize", 0);
        if (player == null) {
            player = this;
            playerLight = this.GetComponentInChildren<Light2D>();
        }
    }

    void Start() { }

    // Update is called once per frame
    void Update() {
        if (!PauseMenu.isPaused && !isDead) {
            if ((int)(playerLight.pointLightOuterRadius * 100) >= PlayerPrefs.GetInt("MaxSize", 0)) {
                PlayerPrefs.SetInt("MaxSize", (int)(playerLight.pointLightOuterRadius * 100));
                Debug.Log("Saved new score");
            }
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
        if (lightRadius < .3f) {
            lightDecreaseSpeed = .005f;
        } else if (lightRadius < .5f) {
            lightDecreaseSpeed = .004f;
        } else if (lightRadius <= 1f) {
            lightDecreaseSpeed = .001f;
            maxSpeed = 4f;
        } else if (lightRadius <= 1.3f) {
            lightDecreaseSpeed = .002f;
            maxSpeed = 3.5f;
        } else if (lightRadius <= 1.7f) {
            lightDecreaseSpeed = .003f;
            maxSpeed = 3f;
        } else if (lightRadius <= 2f) {
            lightDecreaseSpeed = .005f;
            maxSpeed = 2.5f;
        } else if (lightRadius <= 2.5f) {
            lightDecreaseSpeed = .008f;
        } else if (lightRadius <= 3f) {
            lightDecreaseSpeed = .01f;
        } else if (lightRadius <= 3.5f) {
            lightDecreaseSpeed = .012f;
        } else {
            lightDecreaseSpeed = .02f;
        }
        playerLight.pointLightOuterRadius -= lightDecreaseSpeed;
        if (playerLight.pointLightOuterRadius <= 0) {
            setDeathFlag();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("TRIGGERED Player: " + other.gameObject.ToString());

        if (other.gameObject.ToString().Contains("Food")) {
            eatFood(other);
        }

        if (other.gameObject.tag == "World") {
            hitWall();
        }

        if (other.gameObject.tag == "WorldEdge") {
            Debug.Log("WHOA THERE " + other.gameObject.name + ", back up!");
            pushBack(.5f);
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

        AudioManager.instance.PlaySound("Wall");
        pushBack(.1f);
    }

    void pushBack(float wallBufferDistance = .1f) {
        var xPosition = transform.position.x;
        var yPosition = transform.position.y;

        // Bounce off the collider a bit to prevent you from going through
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
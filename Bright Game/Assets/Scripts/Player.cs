using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour {

    public static float speed = 1f;
    public static Player player;
    public Light2D playerLight;
    private float lightDecreaseSpeed = .0001f;


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
        decreasePlayerLight();
        //Debug.Log("Player light radius: " + playerLight.pointLightOuterRadius);
    }

    /*
     * Checks if the player should move, and gives it some movement if it should, with some acceleration
     * at the beginning. If player should stop moving, resets the speed so acceleration can start fresh.
     */
    void checkMovement() {
        var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += (Vector3)move * speed * Time.deltaTime;
        if (move != Vector2.zero && speed < 2f) {
            speed += .01f;
        } else if (move == Vector2.zero) {
            speed = 1;
        }
    }

    void decreasePlayerLight() {
        if (playerLight.pointLightOuterRadius <= 1) {
            lightDecreaseSpeed = .0001f;
        } else if (playerLight.pointLightOuterRadius <= 1.3) {
            lightDecreaseSpeed = .0002f;
        } else if (playerLight.pointLightOuterRadius <= 1.7) {
            lightDecreaseSpeed = .0003f;
        } else {
            lightDecreaseSpeed = .0005f;
        }
        playerLight.pointLightOuterRadius -= lightDecreaseSpeed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("collision");
        Debug.Log(other.gameObject.ToString());
        if (other.gameObject.ToString().Contains("Food")) {
            Debug.Log("Success");
            playerLight.pointLightOuterRadius += .3f;
        }
        other.gameObject.SetActive(false);
        //Destroy(other.gameObject);
        FoodController.instance.SpawnFood();

    }

}
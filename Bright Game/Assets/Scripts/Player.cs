using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour {

    public static float speed = 1f;
    public static Player player;


    void Awake() {
        if (player == null) {
            player = this;
        }
    }

    void Start() { }

    // Update is called once per frame
    void Update() {
        checkMovement();
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

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("collision");
        Debug.Log(other.gameObject.ToString());
        if (other.gameObject.ToString().Contains("Food")) {
            Debug.Log("Success");
            this.GetComponentInChildren<Light2D>().pointLightOuterRadius += .3f;
        }
        other.gameObject.SetActive(false);
        Destroy(other.gameObject);
        FoodController.instance.SpawnFood();

    }

}
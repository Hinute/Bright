using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FoodController : MonoBehaviour {

    public GameObject food;
    public GameObject cloneFood;
    public int sortingOrder = 0;
    public static FoodController instance;
    private Vector2 screenBounds;
    private Light2D foodLight;

    private void Awake() {
        if (instance == null) { // if the instance var is null this is first AudioManager
            instance = this; //save this AudioManager in instance 
        } else {
            Destroy(gameObject); // this isnt the first so destroy it
            return; // since this isn't the first return so no other code is run
        }
    }

    // Start is called before the first frame update
    void Start() {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        SpawnFood();
    }

    void Update() {
        Debug.Log("Food light radius: " + foodLight.pointLightOuterRadius);
        foodLight.pointLightOuterRadius -= .001f;
    }

    public void SpawnFood() {
        if (cloneFood != null) {
            Destroy(cloneFood);
        }
        cloneFood = Instantiate(food);
        foodLight = cloneFood.GetComponentInChildren<Light2D>();
        cloneFood.transform.position = new Vector2((Random.Range(-screenBounds.x, screenBounds.x)), (Random.Range(-screenBounds.y, screenBounds.y)));
    }
}
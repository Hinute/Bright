using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FoodController : MonoBehaviour {

    public GameObject food;
    public List<GameObject> cloneFoods;
    public int sortingOrder = 0;
    public static FoodController instance;
    private Vector2 screenBounds;
    public Light2D foodLight;
    public float respawnTime = 5f;

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
        StartCoroutine(timedFoodSpawn());
    }

    void Update() {
        //Debug.Log("Food light radius: " + foodLight.pointLightOuterRadius);
        foodLight.pointLightOuterRadius -= .001f;
        if (foodLight.pointLightOuterRadius <= 0) {
            Destroy(cloneFood);
        }
    }

    public void SpawnFood() {
        // if (cloneFood != null) {
        //     Destroy(cloneFood);
        // }
        cloneFood = Instantiate(food);
        foodLight = cloneFood.GetComponentInChildren<Light2D>();
        foodLight.pointLightOuterRadius = Random.Range(.4f, 2f);
        cloneFood.transform.position = new Vector2((Random.Range(-screenBounds.x, screenBounds.x)), (Random.Range(-screenBounds.y, screenBounds.y)));
    }

    IEnumerator timedFoodSpawn() {
        while (true) {
            respawnTime = Random.Range(.8f, 5f);
            yield return new WaitForSeconds(respawnTime);
            SpawnFood();
        }
    }
}
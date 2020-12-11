using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FoodController : MonoBehaviour {

    public GameObject blueFood;
    public GameObject greenFood;
    public GameObject orangeFood;
    public GameObject purpleFood;
    public GameObject redFood;
    public GameObject yellowFood;
    public List<GameObject> cloneFoods;
    public GameObject cloneFood;
    public int sortingOrder = 0;
    public static FoodController instance;
    private Vector2 screenBounds;
    public Light2D foodLight;
    public float respawnTime = 5f;
    private List<Color32> colors = new List<Color32>();


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
        screenBounds = gameObject.GetComponent<Renderer>().bounds.size;
        SpawnFood();
        StartCoroutine(timedFoodSpawn());
    }

    void Update() {
        if (!PauseMenu.isPaused && !Player.isDead) {
            for (int i = 0; i < cloneFoods.Count; i++) {
                foodLight = cloneFoods[i].GetComponentInChildren<Light2D>();
                foodLight.pointLightOuterRadius -= .001f;
                if (foodLight.pointLightOuterRadius <= 0) {
                    Destroy(cloneFoods[i]);
                    cloneFoods.RemoveAt(i);
                }
            }
        }
    }

    public void SpawnFood() {

        GameObject[] foods = {blueFood, greenFood, orangeFood, yellowFood, redFood, purpleFood};
        System.Random random = new System.Random();
        int randomIndex = random.Next(foods.Length);

        cloneFood = Instantiate(foods[randomIndex]);
        cloneFoods.Add(cloneFood);

        cloneFood.GetComponentInChildren<Light2D>().pointLightOuterRadius = UnityEngine.Random.Range(.4f, 2f);

        cloneFood.transform.position = new Vector2((UnityEngine.Random.Range(-(screenBounds.x / 2), (screenBounds.x / 2))), (UnityEngine.Random.Range(-(screenBounds.y / 2), (screenBounds.y / 2))));
    }

    IEnumerator timedFoodSpawn() {
        Debug.Log("Entering timedFoodSpawn");
        while (true && !Player.isDead) {
            Debug.Log("Spawn food");
            respawnTime = UnityEngine.Random.Range(.8f, 5f);
            yield return new WaitForSeconds(respawnTime);
            SpawnFood();
        }
        Debug.Log("Not spawning food, player is dead?");
    }

    public void DestroyObject(GameObject gameObject) {
        cloneFoods.Remove(gameObject);
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FoodController : MonoBehaviour {

    public GameObject food;
    public List<GameObject> cloneFoods;
    public GameObject cloneFood;
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
        screenBounds = gameObject.GetComponent<Renderer>().bounds.size;
        SpawnFood();
        StartCoroutine(timedFoodSpawn());
    }

    void Update() {
        if (!PauseMenu.isPaused && !Player.isDead) {
            for (int i = 0; i < cloneFoods.Count; i++) {
                Light2D foodLight = cloneFoods[i].GetComponentInChildren<Light2D>();
                foodLight.pointLightOuterRadius -= .001f;
                if (foodLight.pointLightOuterRadius <= 0) {
                    Destroy(cloneFoods[i]);
                    cloneFoods.RemoveAt(i);
                }
            }
        }
    }

    public void SpawnFood() {
        cloneFood = Instantiate(food);
        cloneFoods.Add(cloneFood);
        cloneFood.GetComponentInChildren<Light2D>().pointLightOuterRadius = Random.Range(.4f, 2f);
        cloneFood.transform.position = new Vector2((Random.Range(-(screenBounds.x / 2), (screenBounds.x / 2))), (Random.Range(-(screenBounds.y / 2), (screenBounds.y / 2))));
    }

    IEnumerator timedFoodSpawn() {
        while (true && !Player.isDead) {
            respawnTime = Random.Range(.8f, 5f);
            yield return new WaitForSeconds(respawnTime);
            SpawnFood();
        }
    }

    public void DestroyObject(GameObject gameObject) {
        cloneFoods.Remove(gameObject);
        Destroy(gameObject);
    }
}
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
    private List<Color32> colors = new List<Color32>();

    private void Awake() {
        setColors();
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

    private void setColors() {
        //red = "#FF0022"
        colors.Add(new Color32(0xFF, 0x00, 0x22, 0xFF));
        //orange = "#FF9900"
        colors.Add(new Color32(0xFF, 0x99, 0x00, 0xFF));
        //yellow = "#FFFF00"
        colors.Add(new Color32(0xFF, 0xFF, 0x00, 0xFF));
        //green = "#00FF00"
        colors.Add(new Color32(0x00, 0xFF, 0x00, 0xFF));
        //blue = "#0000FF"
        colors.Add(new Color32(0x00, 0x00, 0xFF, 0xFF));
        //purple = "#AA00FF";
        colors.Add(new Color32(0xAA, 0x00, 0xFF, 0xFF));
    }

    public void SpawnFood() {
        Color32 foodLightColor = colors[Random.Range(0, 5)];
        Color32 foodSpriteColor = new Color32(foodLightColor.r, foodLightColor.g, foodLightColor.b, 0x77);

        cloneFood = Instantiate(food);
        cloneFoods.Add(cloneFood);

        cloneFood.GetComponentInChildren<SpriteRenderer>().color = foodSpriteColor;
        cloneFood.GetComponentInChildren<Light2D>().pointLightOuterRadius = Random.Range(.4f, 2f);
        cloneFood.GetComponentInChildren<Light2D>().color = foodLightColor;

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
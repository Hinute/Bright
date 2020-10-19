using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {

    public GameObject food;
    public int sortingOrder = 0;
    public static FoodController instance;
    public int foodXCoord = 0;
    public int foodYCoord = 1;
    private Vector2 screenBounds;

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

    public void SpawnFood() {
        Instantiate(food);
        food.transform.position = new Vector2((Random.Range(-screenBounds.x,screenBounds.x)), (Random.Range(-screenBounds.y,screenBounds.y)));
    }

    // Update is called once per frame
    void Update() {

    }
}
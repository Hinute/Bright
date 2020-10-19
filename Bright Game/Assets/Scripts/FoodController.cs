using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {

    public GameObject food;
    public int sortingOrder = 0;
    public static FoodController instance;
    public int foodXCoord = 0;
    public int foodYCoord = 1;

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
        SpawnFood();
    }

    public void SpawnFood() {
        Instantiate(food, new Vector2(foodXCoord, foodYCoord), Quaternion.identity);
    }

    // Update is called once per frame
    void Update() {

    }
}
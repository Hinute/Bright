using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {

    public GameObject food;
    public int sortingOrder = 0;
    public static FoodController instance;

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
        SpawnFood(2, 0);
        //SpawnFood(1, 1);
    }

    public void SpawnFood(int xCoord, int yCoord) {
        Instantiate(food, new Vector2(xCoord, yCoord), Quaternion.identity);
        food.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
    }

    // Update is called once per frame
    void Update() {

    }
}
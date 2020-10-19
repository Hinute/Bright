using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
  private Vector2Int foodGridPosition;
  private int width;
  private int height;

  private Vector2 screenBounds;
  public GameObject foodPrefab;
  public float respawnTime = 1.0f;

  //public LevelGrid(int width, int height){
    //this.width = width;
    //this.height = height;
  //}
  void Start (){
    screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    StartCoroutine(lightFood());

  }

  private void SpawnFood() 
  {
    GameObject food = Instantiate(foodPrefab) as GameObject;
    food.transform.position = new Vector2((Random.Range(-screenBounds.x,screenBounds.x)), (Random.Range(-screenBounds.y,screenBounds.y)));
    //foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));

   // GameObject foodGameObject = new GameObject("Food", typeof(SpriteRenderer))
  }
  IEnumerator lightFood()
  {
    while (true){
      yield return new WaitForSeconds(respawnTime);
      SpawnFood();
    }
  }
}

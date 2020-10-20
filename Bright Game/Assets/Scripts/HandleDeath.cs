using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandleDeath : MonoBehaviour
{

    public GameObject deathScreen;
    public Text maxSize;
    private bool updated = false;
    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        updated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.isDead && !updated){
            deathScreen.SetActive(true);
            maxSize.text += " " + PlayerPrefs.GetInt("MaxSize", 100);
            updated = true;
        }
    }
}

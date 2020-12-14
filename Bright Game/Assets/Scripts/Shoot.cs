using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] ParticleSystem shootParticle = null;
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootLight();
        }

    }

    public void shootLight()
    {
        shootParticle.Play();
        //play shoot sound
    }
}

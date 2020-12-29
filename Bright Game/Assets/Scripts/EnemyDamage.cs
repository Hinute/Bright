using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private int hitCount = 0;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("hit");
        hitCount++;
        if (hitCount >= 20)
        {
            gameObject.SetActive(false);
        }

    }
}

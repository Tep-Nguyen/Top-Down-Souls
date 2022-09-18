using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseBullet : MonoBehaviour
{

   public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player") 
        { 
            if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealthComponent))
            {
                enemyHealthComponent.TakeDamage(10);
            }

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
    }

}

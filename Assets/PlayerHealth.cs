using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static float health = 0f;
    public static float maxHealth = 100f;

    void Start ()
    {
        health = maxHealth;
    }

    public void UpdateHealth(float mod) {
        health += mod;

        if (health > maxHealth) {
            health = maxHealth;
        } else if (health <= 0) {
            health = 0f;
            Destroy(gameObject);
        }
    }
}

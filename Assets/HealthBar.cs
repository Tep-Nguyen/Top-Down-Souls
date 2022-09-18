using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public float playerHealth;
    public float playerMaxHealth;


    void Update()
    {
    playerHealth = PlayerHealth.health;
    playerMaxHealth = PlayerHealth.maxHealth;

    healthBarImage.fillAmount = Mathf.Clamp(playerHealth / playerMaxHealth, 0, 1f);
    }
}
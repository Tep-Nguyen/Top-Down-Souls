using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Image staminaBarImage;
    public float playerStamina;
    public float playerMaxStamina;


    void Update()
    {
        playerStamina = PlayerStamina.stamina;
        playerMaxStamina = PlayerStamina.maxStamina;

        staminaBarImage.fillAmount = Mathf.Clamp(playerStamina / playerMaxStamina, 0, 1f);
    }
}

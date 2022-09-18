using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public static float stamina = 0f;
    public static float maxStamina = 100f;

    [SerializeField] private float staminaRegenCD = 2f;
    private float regenStaminaCounter;
    [SerializeField] private float staminaRechargeRate = 0.025f;

    void Start()
    {
        StartCoroutine(RegenStamina());
        regenStaminaCounter = 0f;
        stamina = maxStamina;
    }

    void Update()
    {
        if (regenStaminaCounter > 0f)
        {
            regenStaminaCounter -= Time.deltaTime;
        }
    }

    public void UpdateStaminaWithCD(float mod)
    {
        stamina += mod;
        regenStaminaCounter = staminaRegenCD;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        else if (stamina <= 0)
        {
            stamina = 0f;
        }
    }

    public void UpdateStaminaNoCD(float mod)
    {
        stamina += mod;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        else if (stamina <= 0)
        {
            stamina = 0f;   
        }
    }

    IEnumerator RegenStamina()
    {
        while (true)
        { 
            if (stamina < 100 && regenStaminaCounter <= 0)
            {
                stamina += 2;
                yield return new WaitForSeconds(staminaRechargeRate);
            }
            else
            { 
                yield return null;
            }
        }
    }
}


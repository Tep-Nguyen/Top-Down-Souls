using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseMoveSpeed = 4f;

    public float dodgeMoveSpeed = 8f;
    public float dodgeTime = 0.5f, dodgeCooldown = 0.5f;
    public float dodgeCost = 20f;

    private float playerStamina;

    //[SerializeField] private float stamina = 0f;
    //[SerializeField] private float maxStamina = 100f;

    private float currentMoveSpeed;

    private bool dodging;
    private float dodgeCounter;
    private float dodgeCoolCounter;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    void Start()
    {
        dodging = false;
        currentMoveSpeed = baseMoveSpeed;
    }

    void Update()
    {
        playerStamina = PlayerStamina.stamina;

        if (!dodging)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            //movement.Normalize();
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Jump"))
        {
            if (dodgeCoolCounter <= 0f && dodgeCounter <= 0f && playerStamina >= 20)
            {
                if (movement.x != 0 || movement.y != 0)
                {
                    this.gameObject.GetComponent<PlayerStamina>().UpdateStaminaWithCD(-dodgeCost);
                    dodging = true;
                    currentMoveSpeed = dodgeMoveSpeed;
                    Physics2D.IgnoreLayerCollision(10, 8, true);
                    dodgeCounter = dodgeTime;
                }
            }
        }

        if (dodgeCounter > 0f)
        {
            dodgeCounter -= Time.deltaTime;

            if (dodgeCounter <= 0f)
            {
                dodging = false;
                currentMoveSpeed = baseMoveSpeed;
                Physics2D.IgnoreLayerCollision(10, 8, false);
                dodgeCoolCounter = dodgeCooldown;
            }
        }

        if (dodgeCoolCounter > 0f)
        {
            dodgeCoolCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentMoveSpeed * Time.fixedDeltaTime);
        //rb.velocity = movement * currentMoveSpeed;

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        
    }
}

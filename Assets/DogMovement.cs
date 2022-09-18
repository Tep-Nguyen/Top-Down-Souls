using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
  
    public float baseSpeed = 3.0f;
    public float chargeSpeed = 10f;
    //public Transform player;
    public GameObject player;
    

    [SerializeField] private float attackDamage = 30f;
    [SerializeField] private float attackSpeed = 0.75f;
    [SerializeField] private float castTime = 1.5f;
    [SerializeField] private float chargeTime = 1f;
    [SerializeField] private float recoveryTime = 1.5f;
    [SerializeField] private float castSpeed = 0.5f;
    [SerializeField] private float recoverySpeed = 1f;



    private float canAttack;
    private bool targetDetected;
    private bool canCharge;
    public bool charging;

    private float currentSpeed;

    private Rigidbody2D rb;
    private Vector2 movement;
    Color tempcolour;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        targetDetected = false;
        canCharge = true;
        currentSpeed = baseSpeed;
        charging = false;
        // this.GetComponent<Renderer>().material.color.a = 1.0f;
        tempcolour = GetComponent<Renderer>().material.color;
        tempcolour.a = 1f;
        GetComponent<Renderer>().material.color = tempcolour;
    }

    void Update()
    {
        //tempcolour = GetComponent<Renderer>().material.color;
        //GetComponent<Renderer>().material.color = tempcolour;

        if (targetDetected == true && canCharge == true)
        {
            StartCoroutine(Charge());
        }
        if (charging == false)
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
            canAttack += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        moveDog(movement);
    }

    void moveDog(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * currentSpeed * Time.deltaTime));
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && charging == true)
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            targetDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            targetDetected = false;
            Debug.Log(targetDetected);
        }
    }

    IEnumerator Charge()
    {
        canCharge = false;
        currentSpeed = castSpeed;
        //tempcolour.a = 1f;
        Debug.Log(tempcolour);
        yield return new WaitForSeconds(castTime);
        currentSpeed = chargeSpeed;
        charging = true;
        yield return new WaitForSeconds(chargeTime);
        currentSpeed = recoverySpeed;
        charging = false;
        //tempcolour.a = 0f;
        Debug.Log(tempcolour);

        yield return new WaitForSeconds(recoveryTime);
        currentSpeed = baseSpeed;
        canCharge = true;
    }
}


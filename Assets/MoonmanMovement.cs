using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonmanMovement : MonoBehaviour
{  
    public float speed = 3.0f;
    //public Transform player;
    public GameObject player;


    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 0.75f;
    private float canAttack;

    private Rigidbody2D rb;
    private Vector2 movement;
    //var player = Player.Singleton.transform;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

    }

    void Update()
    {

        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        canAttack += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        moveMoonman(movement);
    }

    void moveMoonman(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            } 
        }
    }
}

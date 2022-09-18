using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject baseBulletPrefab;

    public float baseBulletForce = 12f;
    public float baseFireRate = 0.4f;

    private bool canShoot = true;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")&&(canShoot))
        {
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject baseBullet = Instantiate(baseBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = baseBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * baseBulletForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(baseFireRate);
        canShoot = true;
    }
}

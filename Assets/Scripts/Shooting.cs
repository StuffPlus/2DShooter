using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Need to call this in order to use UI specific functions.

public class Shooting : MonoBehaviour
{
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Text ammoCounter; // Add a public variable for the text ui

    public Transform firePoint; 
    public GameObject bulletPrefab;

    public float bulletSpeed = 20f;
    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    private void Update()
    {
        ammoCounter.text = currentAmmo.ToString(); // Assigns the current ammo to the text UI
        if (isReloading)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(reload());
            return;
        }
        if (Input.GetButtonDown("Fire1")) // Fire1 is defaulted to the left mouse button
        {

            Shoot(); // Calls the shoot subroutine
        }
    }
    IEnumerator reload()
    {
        isReloading = true;
        Debug.Log("reloading");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    private void Shoot()
    {
        currentAmmo--;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //spawn in bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // get rigidbody2d component
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse); // add a force in the right vector multiplied by the bullet speed.
    }
}

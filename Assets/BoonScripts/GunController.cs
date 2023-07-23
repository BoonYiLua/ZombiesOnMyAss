using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int maxAmmo = 30;
    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;
    public float bulletSpeed = 20f;

    private int currentAmmo;
    private bool isReloading = false;
    private float nextFireTime = 0f;

    private LaserSight laserSight; // Reference to the LaserSight script

    private void Start() {
        currentAmmo = maxAmmo;
    }

    private void Update() {
        if (isReloading)
            return;

        if (currentAmmo <= 0) {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) {
            nextFireTime = Time.time + .1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Reload());
        }
    }

    private void Shoot() {
        // Instantiate the bullet prefab at the FirePoint position and rotation
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the bullet.
        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();

        // Calculate the direction to shoot (straight, in front)
        Vector3 shootDirection = firePoint.forward;

        // Add a force to the bullet to move it in the calculated direction with a certain speed.
        bulletRigidbody.AddForce(shootDirection * bulletSpeed, ForceMode.VelocityChange);

        // Reduce ammo
        currentAmmo--;
    }

    private IEnumerator Reload() {
        isReloading = true;

        // Perform reload animation or logic here
        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    public void AddAmmo(int amount) {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
    }
}


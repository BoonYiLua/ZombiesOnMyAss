using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int magazineSize = 10; // The maximum ammo capacity in the magazine.
    public int maxAmmo = 100; // The maximum total ammo.
    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;
    public float bulletSpeed = 20f;

    private int currentAmmo; // The current total ammo.
    private int currentMagazineAmmo; // The current ammo in the magazine.
    private bool isReloading = false;
    private float nextFireTime = 0f;

    private void Start() {
        currentAmmo = maxAmmo;
        currentMagazineAmmo = magazineSize;
    }

    private void Update() {
        if (currentMagazineAmmo <= 0 && !isReloading && Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Reload());
            return;
        }

        // Check if the player is reloading, and return if true
        if (isReloading) {
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) {
            nextFireTime = Time.time + .1f / fireRate;
            Shoot();
        }
    }

    private void Shoot() {
        if (currentMagazineAmmo > 0) {
            // Instantiate the bullet prefab at the FirePoint position and rotation
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Get the Rigidbody component of the bullet.
            Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();

            // Calculate the direction to shoot (straight, in front)
            Vector3 shootDirection = firePoint.forward;

            // Add a force to the bullet to move it in the calculated direction with a certain speed.
            bulletRigidbody.AddForce(shootDirection * bulletSpeed, ForceMode.VelocityChange);

            // Reduce ammo from the magazine
            currentMagazineAmmo--;
        }
    }

    private IEnumerator Reload() {
        if (currentAmmo > 0 && currentMagazineAmmo < magazineSize) {
            isReloading = true;

            int ammoToReload = Mathf.Min(magazineSize - currentMagazineAmmo, currentAmmo);
            currentMagazineAmmo += ammoToReload;
            currentAmmo -= ammoToReload;

            // Perform reload animation or logic here
            Debug.Log("Reloading...");

            yield return new WaitForSeconds(reloadTime);

            isReloading = false;
        }
    }

    public void AddAmmo(int amount) {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
    }
}

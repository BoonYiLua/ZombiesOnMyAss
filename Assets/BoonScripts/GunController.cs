using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunController : MonoBehaviour {
    public GameObject bulletPrefab;
    public List<Transform> firePoints; // List of firepoints for the gun (optional)
    public int magazineSize = 10; // The maximum ammo capacity in the magazine.
    public int maxAmmo = 100; // The maximum total ammo.
    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;
    public float bulletSpeed = 20f;
    public int bulletsPerShot = 1; // Number of bullets fired per shot

    [SerializeField] private int currentAmmo; // The current total ammo.
    [SerializeField] private int currentMagazineAmmo; // The current ammo in the magazine.
    private bool isReloading = false;
    private float nextFireTime = 0f;

    private void Start() {
        currentAmmo = maxAmmo;
        currentMagazineAmmo = magazineSize;
    }

    private void Update() {
        if (currentMagazineAmmo <= 0 && !isReloading) {
            StartCoroutine(Reload());
            return;
        }

        // Check if the player is reloading, and return if true
        if (isReloading) {
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) {
            nextFireTime = Time.time + .1f / fireRate;
            for (int i = 0; i < bulletsPerShot; i++) {
                Shoot();
            }
        }

        // Check if the player wants to manually reload
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo > 0 && currentMagazineAmmo < magazineSize) {
            StartCoroutine(Reload());
        }
    }

    private void Shoot() {
        if (currentMagazineAmmo > 0) {
            foreach (Transform firePoint in firePoints) {
                // Instantiate the bullet prefab at the FirePoint position and rotation
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                // Get the Rigidbody component of the bullet.
                Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();

                // Calculate the direction to shoot (straight, in front)
                Vector3 shootDirection = firePoint.forward;

                // Add a force to the bullet to move it in the calculated direction with a certain speed.
                bulletRigidbody.AddForce(shootDirection * bulletSpeed, ForceMode.VelocityChange);
            }

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
        // If the current ammo + amount exceeds the max ammo, add the excess ammo to the current magazine ammo
        if (currentAmmo + amount > maxAmmo) {
            int excessAmmo = (currentAmmo + amount) - maxAmmo;
            currentMagazineAmmo = Mathf.Clamp(currentMagazineAmmo + excessAmmo, 0, magazineSize);
            currentAmmo = maxAmmo;
        } else {
            currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
        }
    }
}

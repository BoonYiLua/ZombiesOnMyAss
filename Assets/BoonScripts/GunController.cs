using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GunController : MonoBehaviour {
    public GameObject bulletPrefab;
    public List<Transform> firePoints;
    public int magazineSize = 10;
    public int maxAmmo = 100;
    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;
    public float bulletSpeed = 20f;
    public int bulletsPerShot = 1;

    [SerializeField] private int currentAmmo;
    [SerializeField] private int currentMagazineAmmo;
    public bool isReloading = false;
    private float nextFireTime = 0f;

    [Header("UI Elements")]
    public Text ammoText;

    private void Start() {
        currentAmmo = maxAmmo;
        currentMagazineAmmo = magazineSize;

        // Update the UI text with the initial ammo values
        UpdateAmmoUI();
    }

    private void Update() {
        if (currentMagazineAmmo <= 0 && !isReloading) {
            StartCoroutine(Reload());
            return;
        }

        if (isReloading) {
            return;
        }

        if (GetComponentInParent<PlayerController>().player.ToString() == "P1") {
            if (Input.GetButton("Fire1")) {
                Fire();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo > 0 && currentMagazineAmmo < magazineSize) {
            StartCoroutine(Reload());
        }
    }

    public void Fire() {
        if (Time.time >= nextFireTime) {
            nextFireTime = Time.time + .1f / fireRate;
            for (int i = 0; i < bulletsPerShot; i++) {
                Shoot();
            }
        }
    }

    private void Shoot() {
        if (currentMagazineAmmo > 0) {
            foreach (Transform firePoint in firePoints) {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
                Vector3 shootDirection = firePoint.forward;
                bulletRigidbody.AddForce(shootDirection * bulletSpeed, ForceMode.VelocityChange);
            }

            currentMagazineAmmo--;
            UpdateAmmoUI(); // Update the UI after each shot
        }
    }

    public IEnumerator Reload() {
        if (currentAmmo > 0 && currentMagazineAmmo < magazineSize) {
            isReloading = true;
            int ammoToReload = Mathf.Min(magazineSize - currentMagazineAmmo, currentAmmo);
            currentMagazineAmmo += ammoToReload;
            currentAmmo -= ammoToReload;
            Debug.Log("Reloading...");

            UpdateAmmoUI(); // Update the UI after the reload

            yield return new WaitForSeconds(reloadTime);

            isReloading = false;
        }
    }

    public void AddAmmo(int amount) {
        if (currentAmmo + amount > maxAmmo) {
            int excessAmmo = (currentAmmo + amount) - maxAmmo;
            currentMagazineAmmo = Mathf.Clamp(currentMagazineAmmo + excessAmmo, 0, magazineSize);
            currentAmmo = maxAmmo;
        } else {
            currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
        }

        UpdateAmmoUI(); // Update the UI after adding ammo
    }

    private void UpdateAmmoUI() {
        ammoText.text = currentMagazineAmmo.ToString() + " / " + currentAmmo.ToString();
    }
}

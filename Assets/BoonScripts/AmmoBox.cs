using UnityEngine;
using System.Collections;

public class AmmoBox : MonoBehaviour {
    public int ammoAmount = 20; // Amount of ammo to give to the player
    public float cooldownTime = 10f; // Cooldown time in seconds

    private bool canClaimAmmo = true;

    public void ClaimAmmo() {
        if (canClaimAmmo) {
            GunController gunController = FindObjectOfType<GunController>();
            if (gunController != null) {
                // Add ammo to the player's current ammo in the gun controller
                gunController.AddAmmo(ammoAmount);
                Debug.Log("Ammo claimed: " + ammoAmount);

                // Disable claiming ammo from this box until cooldown finishes
                canClaimAmmo = false;

                // Start the cooldown coroutine
                StartCoroutine(AmmoCooldown());
            }
        }
    }

    private IEnumerator AmmoCooldown() {
        yield return new WaitForSeconds(cooldownTime);

        // Re-enable claiming ammo from this box after the cooldown
        canClaimAmmo = true;
    }
}
using UnityEngine;
using System.Collections;

public class AmmoBox : MonoBehaviour {
    public int ammoAmount = 20; // Amount of ammo to give to the player

    public void ClaimAmmo() {
        GunController gunController = FindObjectOfType<GunController>();
        if (gunController != null) {
            // Add ammo to the player's current ammo in the gun controller
            gunController.AddAmmo(ammoAmount);
            Debug.Log("Ammo claimed: " + ammoAmount);
        }
    }
}

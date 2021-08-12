using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") //If the player collides with the ammo pickupable
        {
            other.GetComponent<PlayerSounds>().PlayReloadSound();

            other.GetComponent<Ammo>().IncreaseAmmo(ammoType, ammoAmount); //Increase the ammo

            Destroy(transform.gameObject); //Destroy the pickupable
        }
    }
}

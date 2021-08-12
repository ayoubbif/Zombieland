using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") //If the player collides with the mana pickupable
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            PlayerSounds playerSounds = other.GetComponent<PlayerSounds>();
            if (playerHealth.GetHealth() <= 99.9f) //If mana is less than 99.9 restore health to a maximum of 50 
            {
                playerHealth.RestoreHealth(50.0f);
                playerSounds.PlayHealthPickupSound();
                Destroy(gameObject); //Destroy the pickupable
            }

        }
    }
}

using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public GameObject[] lootObjects;//Objects which can be dropped as loot
    public GameObject healthPickup, bulletPickup, shellsPickup, batteryPickup, player;
    [SerializeField] FlashlightSystem flashlight;


    public int chance = 100;

    public void DropLoot(GameObject enemy)
    {
        Vector3 dropPosition = enemy.transform.position;
        int lootNumber = Random.Range(0, lootObjects.Length);
        GameObject lootToDrop;
        float playerHealth = player.GetComponent<PlayerHealth>().GetHealth();
        int bullets = player.GetComponent<Ammo>().GetAmmoCount(AmmoType.Bullets);
        int shells = player.GetComponent<Ammo>().GetAmmoCount(AmmoType.Shells);
         
            //Drop the thing the player needs the most,
            //If about to die, drop health, or whichever weapon is lower

        if (playerHealth <= 25)
        {
            lootToDrop = healthPickup;
        }
        else if (shells <= 2 || bullets <= 4)
        {
            if (shells < bullets)
            {
                lootToDrop = shellsPickup;
            } 
            else
            {
                lootToDrop = bulletPickup;
            }
        }
        else if(flashlight.GetLightIntensity() < 1)
        {
            lootToDrop = batteryPickup;
        }
        else 
        {
            lootToDrop = lootObjects[lootNumber];
        }
        
        InstantiateLoot(lootToDrop, dropPosition);
    }
    private void InstantiateLoot(GameObject loot, Vector3 location)
    {
        Instantiate(loot, location + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
    }
}

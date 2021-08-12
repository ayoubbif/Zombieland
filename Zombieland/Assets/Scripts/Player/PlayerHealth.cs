using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    [SerializeField] float health = 100f;
    public Slider HealthBar;

    private void Awake()
    {
        singleton = this;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; //Decrease health depending on the damage amount
        HealthBar.value = health; //Decrease the healthbar value

        if (health <= 0)
        {
            KillPlayer();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void RestoreHealth(float healthAmount)
    {
        health += healthAmount;
        if (health >= 100.0f)
        {
            health = 100.0f;
        }
        HealthBar.value = health;
    }

    private void KillPlayer()
    {
        GetComponent<DeathHandler>().HandleDeath();
    }
}
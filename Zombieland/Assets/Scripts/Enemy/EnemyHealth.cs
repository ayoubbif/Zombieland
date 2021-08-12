using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    public Transform player;
    bool isDead = false;
    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(float damageAmmount) 
    {
        BroadcastMessage("OnDamageTaken"); //Emit the OnDamageTaken message when the TakeDamage is called
        hitPoints -= Random.Range(damageAmmount/2, damageAmmount);

        if (hitPoints <= 0) 
        {
            Die();
        }
    }

    //Setting the difficulty level
    public void SetDifficulty(DifficultyLevel level) 
    {
        if (level == DifficultyLevel.Easy)
        {
            hitPoints = hitPoints * 1f;
        }
        if (level == DifficultyLevel.Easy)
        {
            hitPoints = hitPoints * 1.5f;
        }
        if (level == DifficultyLevel.Hard)
        {
            hitPoints = hitPoints * 2f;
        }
    }

    public bool IsDead() 
    {
        return isDead;
    }
    private void Die()
    {
        if (isDead) return;
        GetComponent<Patrol>().StopPatroling(); //Deactivate the patrol
        GetComponent<CapsuleCollider>().enabled = false; //Deactivate the collider
        GetComponent<LootDrop>().DropLoot(gameObject); //Drop a pickupable loot (ex: Bullets, Mana etc..)
        GetComponent<Animator>().SetBool("Dead", true); //Activate death animation
        isDead = true;

        //Emit the OnEnemyKilled event if it isn't null 
        if(OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
    }
}
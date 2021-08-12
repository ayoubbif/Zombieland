using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public static EnemyAttack singleton;
    PlayerHealth target;
    [SerializeField] float damage = 15f;

    private void Awake()
    {
        singleton = this; //Singleton to access EnemyAttackHitEvent in EnemyAI
    }
    private void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AtttackHitEvent()
    {
        if (target == null) { return; }
        PlayerHealth.singleton.TakeDamage(Random.Range(damage / 2, damage)); //Player gets random damage based on the damage amount defined
        target.GetComponent<DamageUI>().ShowDamageUI(); //Display the blood UI
    }
}
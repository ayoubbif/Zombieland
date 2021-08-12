using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    EnemyHealth health;
    NavMeshAgent agent;
    EnemySounds sounds;
    Animator animator;

    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float chaseRange = 5.0f;
    [SerializeField] float idleSpeed = 1.0f;
    [SerializeField] float chaseSpeed = 3.5f;
    [SerializeField] float soundRange = 5.0f;
    [SerializeField] float notificationRadius = 10.0f;

    bool isProvoked = false;
    public bool canAttack = true;
    bool nearbySoundPlayed = false;   
    float distanceToTarget = Mathf.Infinity;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
        sounds = GetComponent<EnemySounds>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health.IsDead()) //If the enemy is dead disable its agent
        {
            enabled = false;
            agent.enabled = false;
            return;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position); //Calculating the distance between the player and enemy

        if (distanceToTarget <= soundRange && !nearbySoundPlayed) //Play the enemy sounds when the distance between the player and enemy is less than sound range
        {
            PlaySounds();
            nearbySoundPlayed = true;
        }
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange) //The enemy is provoked if the distance is less than chase range
        {
            if (isPlayerVisible() == true)
            {
                isProvoked = true;
            }
        }
    }

    private void PlaySounds()
    {
        sounds.PlayZombieAttack();
    }

    public void Provoke()
    {
        isProvoked = true;
    }
    private void TellOtherEnemies() //Notify nearby enemies by sending the message provoke when an enemy is hit
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, notificationRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                hitColliders[i].SendMessage("Provoke");
            }
            i++;
        }
    }
    public bool isPlayerVisible() //Use a raycast to determine if the player is in sight
    {
        Vector3 direction = target.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();
            if (player == null) { return false; }
        }
        return true;
    }

    public void OnDamageTaken() 
    {
        isProvoked = true;
        TellOtherEnemies();
    }
    private void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= agent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= agent.stoppingDistance && canAttack)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        animator.SetBool("Attack", true); //Set attack animation to true
        agent.speed = idleSpeed; //Set enemy speed to idle speed
        StartCoroutine(AttackTimer()); 
    }

    IEnumerator AttackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f); //Wait for 0.5 seconds before attacking
        EnemyAttack.singleton.AtttackHitEvent();
        yield return new WaitForSeconds(2f);//Wait for 2 seconds after attacking
        canAttack = true;
    }

    private void ChaseTarget()
    {
        animator.SetBool("Attack", false); //Set the attack animation to false
        animator.SetTrigger("Walk"); //Active the walk trigger
        agent.SetDestination(target.position);//Set enemy's destination toward the player's position
        agent.speed = chaseSpeed; //Set enemy's speed to chase speed

    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; //Get the normalized direction
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); //Defining the enemy's look rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed); //Facing the target using the Slerp method
    }

    void OnDrawGizmosSelected() //Display notification radius
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
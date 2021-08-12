using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject enemy;

    private void Start()
    {
        SpawnEnemy();
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += SpawnEnemy; //Listen to the OnEnemyKilledEvent
    }

    private void SpawnEnemy()
    {
        int spawnerIndex = Random.Range(0, spawners.Length); //Choose randomly a point to spawn
        Instantiate(enemy, spawners[spawnerIndex].transform.position, spawners[spawnerIndex].transform.rotation); //Instantiate a clone of an enemy
    }

}

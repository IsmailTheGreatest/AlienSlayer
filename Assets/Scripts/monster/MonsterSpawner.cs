using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnRate = 2.0f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public int maxMonsters = 5; // Maximum number of monsters to spawn
    private int spawnedMonsters = 0; // Number of monsters spawned
    private int destroyedMonsters = 0; // Number of monsters destroyed

    public int currentLevelIndex;

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (spawnedMonsters < maxMonsters)
        {
            SpawnMonster();
            spawnedMonsters++;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnMonster()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        monster.GetComponent<monster>().spawner = this;
    }

    public void OnMonsterDestroyed()
    {
        destroyedMonsters++;

        // Check if all spawned monsters are destroyed
        if (destroyedMonsters >= maxMonsters)
        {
            GameManager.Instance.MarkLevelComplete(currentLevelIndex);
        }
    }
}

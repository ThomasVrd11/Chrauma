using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

// * hello nicolas

public class Spawner : MonoBehaviour
{
	// * Array of spawn points
	[SerializeField] private Transform[] spawnPoints;
	[SerializeField] private Transform spawnPointsHidden;
	[SerializeField] private float timeBetweenSpawns = 5f;
	[SerializeField] private int maxEnemies = 5;
	[SerializeField] private int maxActiveEnemies = 3;
	[SerializeField] private Enemy enemyPrefab;
	[SerializeField] private float navMeshSampleDistance = 1.0f;

	private float timeSinceLastSpawn;
	private int totalSpawnedEnemies;
	private int currentActiveEnemies;
	private IObjectPool<Enemy> enemyPool;
	[SerializeField] public bool isSpawningActive;
	public int numberOfKilledEnnemies;
	[Tooltip("Number of enemy to spawn before stopping")]
	public int numberOfEnnemiesNeeded;
	public bool debugMode;

	private void Awake()
	{
		enemyPool = new ObjectPool<Enemy>(CreateEnemy, OnGetEnemy, OnReleaseEnemy);
		PreSpawnEnemies();
		isSpawningActive = false;
	}

	// * using the ObjectPool to create a new enemy
	// * and set the pool to the enemy
	private Enemy CreateEnemy()
	{
		Enemy enemy = Instantiate(enemyPrefab, spawnPointsHidden.position, Quaternion.identity);
		enemy.SetPool(enemyPool);
		enemy.gameObject.SetActive(false);
		return enemy;
	}
	// * using the ObjectPool to get an enemy
	// * and set the position of the enemy
	private void PreSpawnEnemies()
	{
		for (int i = 0; i < maxEnemies; i++)
		{
			Enemy enemy = CreateEnemy();
			enemyPool.Release(enemy);
		}
	}
	private void OnGetEnemy(Enemy enemy)
	{
		if (currentActiveEnemies >= maxActiveEnemies) return;
		enemy.currentHealth = enemy.startingHealth;

	}

	private void OnReleaseEnemy(Enemy enemy)
	{
		enemy.gameObject.SetActive(false);
		enemy.transform.position = spawnPointsHidden.position;
		if (currentActiveEnemies > 0)
		{
			currentActiveEnemies--;
			numberOfKilledEnnemies++;
			if (debugMode) Debug.Log(numberOfKilledEnnemies);
		}
	}
	void Update()
	{
		if (isSpawningActive && Time.time > timeSinceLastSpawn && currentActiveEnemies < maxActiveEnemies && totalSpawnedEnemies < numberOfEnnemiesNeeded)
		{
			SpawnEnemyAtPoint();
			timeSinceLastSpawn = Time.time + timeBetweenSpawns;
			if (debugMode) Debug.Log("current active " + currentActiveEnemies);
		} else if ( totalSpawnedEnemies >= numberOfEnnemiesNeeded) StopSpawning();
	}
	public void SpawnEnemyAtPoint()
	{
		int spawnIndex = Random.Range(0, spawnPoints.Length);
		if (spawnIndex < 0 || spawnIndex >= spawnPoints.Length)
		{
			Debug.LogError("Invalid spawn point spawnIndex");
			return;
		}

		if (currentActiveEnemies < maxActiveEnemies && totalSpawnedEnemies < maxEnemies)
		{
			Vector3 spawnPosition = GetValidNavMeshPosition(spawnPoints[spawnIndex].position);
			if (spawnPosition != Vector3.zero)
			{
				Enemy enemy = enemyPool.Get();
				enemy.transform.position = spawnPosition;
				NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
				if (agent)
				{
					agent.Warp(spawnPosition);
				}
				enemy.gameObject.SetActive(true);
				currentActiveEnemies++;
				totalSpawnedEnemies++;
			}
			else
			{
				Debug.LogWarning("No valid navmesh position to spawn");
			}
		}
	}
	private Vector3 GetValidNavMeshPosition(Vector3 position)
	{
		NavMeshHit hit;
		if (NavMesh.SamplePosition(position, out hit, navMeshSampleDistance, NavMesh.AllAreas))
		{
			return hit.position;
		}
		return Vector3.zero;
	}

	public void StartSpawning()
	{
		if (debugMode) Debug.Log("start spawning");
		isSpawningActive = true;
		totalSpawnedEnemies = 0;
		numberOfKilledEnnemies = 0;
	}
	public void StopSpawning()
	{
		isSpawningActive = false;
	}
}
// * In EnemyAIMelee.cs, the enemy is Released after getting killed
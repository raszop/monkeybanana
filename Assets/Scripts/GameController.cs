using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private ObjectPoolManager bulletsPool;
    [SerializeField]
    private ObjectPoolManager enemiesPool;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerMovementController playerMovementController;
    [SerializeField]
    private PlayerShootingController playerShootingController;
    [SerializeField]
    private GameplayUIController uiController;

    private Vector2 startingPlayerPosition = new Vector2(0F, -3F);
    private Vector3 difficultyIncreaseVector = new Vector3(0, 1, 0);
    private float startingSpawnerEdgeHeight;

    private float maxEnemySpawnWaitTime = 3.0F;

    private WaitForSeconds waitTime = new WaitForSeconds(2F);

    private int score = 0;
    private int lives = 3;

    public TwoPointSpawner[] enemySpawnerArray;

    private void Awake()
    {
        EnemyDamageController.onEnemyDeath += IncreaseScore;
        PlayerShootingController.onShootEvent += ShootBullet;
        PlayerDamageController.onHitByEnemy += PlayerReceiveDamage;
    }

    private void OnDestroy()
    {
        EnemyDamageController.onEnemyDeath -= IncreaseScore;
        PlayerShootingController.onShootEvent -= ShootBullet;
        PlayerDamageController.onHitByEnemy -= PlayerReceiveDamage;
    }

    private void Start()
    {
        startingSpawnerEdgeHeight = enemySpawnerArray[0].secondEdge.position.y;
        StartNewGame();
    }

    public void StartNewGame()
    {
        CancelInvoke();
        lives = 3;
        score = 0;
        uiController.RefreshScoreUI(score);
        uiController.SetupLivesUI();
        bulletsPool.InitializePool();
        enemiesPool.InitializePool();
        player.transform.position = startingPlayerPosition;
        ResetDifficulty();
        Invoke(nameof(SpawnEnemy), Random.Range(1, 4));
        Invoke(nameof(IncreaseDifficulty), 5.0F);
    }
      
    private void IncreaseScore()
    {
        score += 1;
        uiController.RefreshScoreUI(score);
    }

    private void PlayerReceiveDamage()
    {
        lives -= 1;
        uiController.ReduceHeartCount(lives);

        if (lives > 0)
        {
            StartCoroutine(PlayerHitRoutine());
        }
        else
        if (lives == 0)
        {
            uiController.ShowGameOverScreen(score);
            CancelInvoke();
            enemiesPool.InitializePool();
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = GetObjectFromPool(bulletsPool);
        bullet.transform.position = player.transform.position;

        bullet.SetActive(true);
    }

    private void SpawnEnemy()
    {
        GameObject enemy = GetObjectFromPool(enemiesPool);
        enemy.transform.position = RandomEnemySpawnPosition();

        enemy.SetActive(true);

        Invoke(nameof(SpawnEnemy), Random.Range(0, maxEnemySpawnWaitTime));
    }

    private Vector2 RandomEnemySpawnPosition()
    {
        int spawnerID = Random.Range(0, enemySpawnerArray.Length);
        return new Vector2(Random.Range(enemySpawnerArray[spawnerID].firstEdge.position.x, enemySpawnerArray[spawnerID].secondEdge.position.x), Random.Range(enemySpawnerArray[spawnerID].firstEdge.position.y, enemySpawnerArray[spawnerID].secondEdge.position.y));
    }

    private GameObject GetObjectFromPool(ObjectPoolManager pool)
    {
        GameObject obj = pool.GetObjectFromQueue();
        pool.AddObjectToQueue(obj);
        return obj;
    }

    private void ResetDifficulty()
    {
        foreach (TwoPointSpawner spawnerEdge in enemySpawnerArray)
        {
            spawnerEdge.secondEdge.transform.position = new Vector3(spawnerEdge.transform.position.x, startingSpawnerEdgeHeight, spawnerEdge.transform.position.z);
        }
    }

    private void IncreaseDifficulty()
    {
        if (enemySpawnerArray[0].secondEdge.transform.position.y > 0)
        {
            Invoke(nameof(IncreaseDifficulty), 5.0F);
        }
        foreach (TwoPointSpawner spawnerEdge in enemySpawnerArray)
        {
            spawnerEdge.secondEdge.transform.position -= difficultyIncreaseVector;
        }
    }

    IEnumerator PlayerHitRoutine()
    {
        enemiesPool.InitializePool();
        SetPlayerControls(false);
        CancelInvoke();
        player.transform.position = startingPlayerPosition;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return waitTime;
        SetPlayerControls(true);
        Invoke(nameof(SpawnEnemy), Random.Range(1, 4));
    }

    private void SetPlayerControls(bool isOn)
    {
        playerMovementController.enabled = isOn;
        playerShootingController.enabled = isOn;
    }
}

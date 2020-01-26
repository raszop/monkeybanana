using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyController;
    
    private const string playerBulletTag = "PlayerBullet";
    
    public delegate void OnEnemyDeath();
    public static event OnEnemyDeath onEnemyDeath;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(playerBulletTag))
        {
            enemyController.ResetGameObject();
            onEnemyDeath?.Invoke();
        }
    }
}

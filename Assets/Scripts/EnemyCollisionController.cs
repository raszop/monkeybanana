using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionController : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyController;

    private const string playerBulletTag = "PlayerBullet";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag(playerBulletTag))
        {
            enemyController.ResetGameObject();
        }
    }
}

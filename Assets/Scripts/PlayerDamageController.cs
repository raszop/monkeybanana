using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    private const string enemyTag = "Enemy";

    public delegate void OnHitByEnemy();
    public static event OnHitByEnemy onHitByEnemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(enemyTag))
        {
            onHitByEnemy?.Invoke();
        }
    }
}

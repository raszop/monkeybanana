using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IResettable
{
    [SerializeField]
    private Sprite[] availableSprites;
    [SerializeField]
    private SpriteRenderer enemySprite;
    [SerializeField]
    private Rigidbody2D rb;

    private float downAcceleration;
    private MovementDirection movementDirection;
    private WaitForSeconds waitForSeconds;

    private const float minScale = 0.5F;
    private const float maxScale = 1.5F;
    private const float minMovementSpeed = 6.0F;
    private const float maxMovementSpeed = 10.0F;
    private const float minDownAcceleration = 0F;
    private const float maxDownAcceleration = 1F;
        
    enum MovementDirection { right, left};

    private void OnEnable()
    {
        RandomizeEnemyLook();
        GenerateEnemyMovement();        
    }

    private void RandomizeEnemyLook()
    {
        enemySprite.sprite = availableSprites[Random.Range(0, availableSprites.Length)];
        float enemyScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector2(enemyScale, enemyScale);
    }

    private void GenerateEnemyMovement()
    {
        movementDirection = transform.position.x > 0 ? MovementDirection.left : MovementDirection.right;
        switch(movementDirection)
        {
            case MovementDirection.right:
                {
                    rb.velocity = Vector2.right * RandomMovementSpeed();
                    break;
                }
            case MovementDirection.left:
                {
                    rb.velocity = Vector2.left * RandomMovementSpeed();
                    break;
                }                
        }

        //waitForSeconds = new WaitForSeconds(Random.Range(0.1F, 1.0F));
        downAcceleration = RandomDownAcceleration();
        rb.gravityScale += RandomDownAcceleration();
        //StartCoroutine(SinkDownRoutine());
    }

    //IEnumerator SinkDownRoutine()
    //{
    //    yield return waitForSeconds;
    //    rb.gravityScale += downAcceleration;
    //}

    private float RandomDownAcceleration()
    {
        return Random.Range(minDownAcceleration, maxDownAcceleration);
    }

    private float RandomMovementSpeed()
    {
        return Random.Range(minMovementSpeed, maxMovementSpeed);
    }

    private void OnBecameInvisible()
    {
        ResetGameObject();
    }

    public void ResetGameObject()
    {
        gameObject.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        StopAllCoroutines();
    }
}

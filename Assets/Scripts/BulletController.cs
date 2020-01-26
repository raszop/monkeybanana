using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IResettable
{
    [SerializeField]
    private Rigidbody2D rb;

    private float bulletSpeed = 10F;
    private Vector3 rotationVector = new Vector3(0, 0, 13);

    private void OnEnable()
    {
        rb.velocity = Vector2.up * bulletSpeed;
    }

    private void Update()
    {
        transform.Rotate(rotationVector);
    }

    private void OnBecameInvisible()
    {
        ResetGameObject();        
    }

    public void ResetGameObject()
    {
        gameObject.SetActive(false);
        rb.velocity = Vector2.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private Vector3 touchPosition;
    private Vector3 moveDirection;
    private float movementSpeed = 10F;

    // Update is called once per frame
    void Update()
    {
        HandleTouchInput();
#if UNITY_EDITOR
        HandleDebugInput();
#endif
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            moveDirection = touchPosition - transform.position;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
            }

        }
    }

    private void HandleDebugInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {

        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector2.right * movementSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector2.up * movementSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = Vector2.down * movementSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = Vector2.left * movementSpeed;
        }

        if (IsAnyKeyUp())
        {
            rb.velocity = Vector2.zero;
        }
    }

    private bool IsAnyKeyUp()
    {
        return Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootingController : MonoBehaviour
{
    private bool shootContinuously = false;
    private bool shootByHolding = false;

    [SerializeField]
    private float shootingRate = 0.7F;
    private float nextShoot = 0.0F;

    public delegate void OnShoot();
    public static event OnShoot onShootEvent;

    private void Start()
    {
        shootContinuously = Utilities.CheckAutoFireSetting();
    }

#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        HandleDebugInput();
    }
#endif

    private void FixedUpdate()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + shootingRate;
            if (IsPlayerSetToShooting())
            {
                onShootEvent?.Invoke();
            }
        }

        shootByHolding = IsPlayerTouchingScreen();
    }

    private bool IsPlayerTouchingScreen()
    {
        return Input.touchCount > 0;
    }

    private bool IsPlayerSetToShooting()
    {
        return shootContinuously || shootByHolding;
    }

    private void HandleDebugInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            shootByHolding = true;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            shootByHolding = false;
        }
    }    
}

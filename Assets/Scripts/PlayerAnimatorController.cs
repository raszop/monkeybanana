using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private string throwAnimationTrigger = "ThrowAnimationTrigger";

    private void Awake()
    {
        PlayerShootingController.onShootEvent += TriggerPlayerThrowAnimation;
    }

    private void OnDestroy()
    {
        PlayerShootingController.onShootEvent -= TriggerPlayerThrowAnimation;
    }

    private void TriggerPlayerThrowAnimation()
    {
        animator.SetTrigger(throwAnimationTrigger);
    }
}

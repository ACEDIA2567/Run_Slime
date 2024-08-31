using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private MoveMent movement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<MoveMent>();
    }

    private void Update()
    {
        // 슬라임이 위로 점프 중일 때
        if (Input.GetKeyDown(KeyCode.W) && movement.isGround)
        {
            animator.SetTrigger("isJumping");
        }

        // 슬라임이 좌우로 이동할 때
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            //transform.localScale = new Vector3(-1f, 1f, 1f);
            animator.SetTrigger("isMoving");
        }
        else
        {
            animator.SetTrigger("isIdle");
        }
    }
}

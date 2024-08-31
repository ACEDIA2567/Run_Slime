using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashHook : MonoBehaviour
{
    private PlayerHook playerHook; // PlayerHook의 정보
    public DistanceJoint2D joint2D;

    private void Start()
    {
        playerHook = GameObject.Find("slime").GetComponent<PlayerHook>(); // 플레이어에 컴포넌트 되어있는 PlayerHook 스크립트의 정보를 받는다.
        joint2D = GetComponent<DistanceJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook"))
        {
            joint2D.enabled = true;
            playerHook.isCrash = true;
        }
    }
}

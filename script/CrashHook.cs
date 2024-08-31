using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashHook : MonoBehaviour
{
    private PlayerHook playerHook; // PlayerHook�� ����
    public DistanceJoint2D joint2D;

    private void Start()
    {
        playerHook = GameObject.Find("slime").GetComponent<PlayerHook>(); // �÷��̾ ������Ʈ �Ǿ��ִ� PlayerHook ��ũ��Ʈ�� ������ �޴´�.
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

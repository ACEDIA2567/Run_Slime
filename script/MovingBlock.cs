using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float speed = 2; // ���� �ӵ�
    private float blockLoc; // ó�� ���� ��ġ
    private float blockLoc2; // ������ ���� ��ġ
    public bool blockXY = false; // ���� x��ǥ�� �̵��� Y��ǥ �̵��� ����
    [SerializeField]
    private float blockDistance = 5.0f; // ���� �̵��Ÿ�

    private Rigidbody2D rid2d;
    [HideInInspector]
    public bool PlayerSame; // �÷��̾�� ���� �ӵ� ���� 

    private void Start()
    {
        rid2d = GetComponent<Rigidbody2D>();
        if ( blockXY )
        {
            blockLoc = transform.position.x;
            blockLoc2 = blockLoc + 5.0f;
        }
        else
        {
            blockLoc = transform.position.y;
        }
    }

    private void Update()
    {
        // X������ �̵�
        if ( blockXY )
        {
            // Y�� ����
            rid2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            rid2d.velocity = new Vector2(speed, 0);
            if (transform.position.x >= blockLoc + blockDistance)
            {
                // ���� �̵��� �ݴ�� ����
                speed *= -1;
                blockLoc = blockLoc2;
                PlayerSame = true;
            }

            else if (transform.position.x <= blockLoc - blockDistance)
            {
                speed *= -1;
                blockLoc -= blockDistance;
                PlayerSame = false;
            }
        }

        // Y������ �̵�
        else
        {
            // X�� ����
            rid2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rid2d.velocity = new Vector2(0, speed);
            if (transform.position.y >= blockLoc + blockDistance)
            {
                // ���� �̵��� �ݴ�� ����
                speed *= -1;
                blockLoc = transform.position.y;
            }

            else if (transform.position.y <= blockLoc - blockDistance)
            {
                speed *= -1;
                blockLoc = transform.position.y;
            }
        }
    }

}

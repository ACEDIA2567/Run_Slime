using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float speed = 2; // 블럭의 속도
    private float blockLoc; // 처음 블럭의 위치
    private float blockLoc2; // 마지막 블럭의 위치
    public bool blockXY = false; // 블럭의 x좌표의 이동과 Y좌표 이동의 선택
    [SerializeField]
    private float blockDistance = 5.0f; // 블럭의 이동거리

    private Rigidbody2D rid2d;
    [HideInInspector]
    public bool PlayerSame; // 플레이어와 같은 속도 여부 

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
        // X축으로 이동
        if ( blockXY )
        {
            // Y축 고정
            rid2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            rid2d.velocity = new Vector2(speed, 0);
            if (transform.position.x >= blockLoc + blockDistance)
            {
                // 블럭의 이동을 반대로 변경
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

        // Y축으로 이동
        else
        {
            // X축 고정
            rid2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rid2d.velocity = new Vector2(0, speed);
            if (transform.position.y >= blockLoc + blockDistance)
            {
                // 블럭의 이동을 반대로 변경
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveMent : MonoBehaviour
{

    public float speed = 5.0f; // 이동 속도
    [SerializeField]
    private float jump = 8.0f;  // 점프 속도
    [HideInInspector] // public 변수를 inspector view에 보이지 않게 설정
    public bool LongJump = false; // 긴 점프와 낮은 점프
    [HideInInspector]
    public bool isdash = false; // 대쉬
    [SerializeField]
    private float dashSpeed = 40.0f;   // 대쉬 속도
    private float DashTime;    // 대쉬 시간
    [HideInInspector]
    public bool isKnockBack; // 넉백 확인
    [HideInInspector]
    public float WarpCount = 0; // 워프 횟수
    [HideInInspector]
    public float dashWaitTime = 0.0f; // 대쉬 쿨타임
    [HideInInspector]
    public bool GetKey = false; // 키 획득
    private bool IsWater = false; // 물 충돌 체크

    [HideInInspector]
    public Rigidbody2D rigid2D;
    public AudioClip audioClip;
    AudioSource audioSource;

    [SerializeField]
    private LayerMask groundLayer;       // 바닥 체크를 위한 레이어
    private BoxCollider2D boxCollider2D; // 오브젝트의 충돌 범위 컴포넌트
    public bool isGround;               // 바닥 체크 (바닥에 닿아있으면 true)
    private Vector2 underLeftPosition;   // 왼쪽 바닥 위치
    private Vector2 underRightPosition;  // 오른쪽 바닥 위치

    private PlayerHook playerHook; // PlayerHook의 정보

    [HideInInspector]
    public Vector2 boxSize = new Vector2(1.1f, 0.3f); // 바닥과의 충돌을 위한 박스                  

    public int JumpCount = 0; // 점프 가능 횟수

    private bool PlayerBlock = false; // 플레이어와 움직이는 발판의 충돌 여부
    private bool MovingPlayer; // 움직이는 발판의 좌우 값
    private float MovingBlockSpeed; // 움직이는 발판의 속도값

    private GameObject Keyui; // 키 아이템 UI
    private GameObject Warpui; // 워프 아이템 UI
    private GameObject Hookui; // 후크 아이템 UI

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        playerHook = GetComponent<PlayerHook>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position + new Vector3(-1.5f, 1.5f, 0), new Vector3(-1.0f, 1.0f, 0), Color.red); // 왼쪽위
        Debug.DrawRay(transform.position + new Vector3(-1.5f, -1.5f, 0), new Vector3(-1.0f, -1.0f, 0), Color.blue); // 왼쪽아래
        Debug.DrawRay(transform.position + new Vector3(1.5f, 1.5f, 0), new Vector3(1.0f, 1.0f, 0), Color.green); // 오른쪽위
        Debug.DrawRay(transform.position + new Vector3(1.5f, -1.5f, 0), new Vector3(1.0f, -1.0f, 0), Color.black); // 오른쪽아래
        Debug.DrawRay(transform.position + new Vector3(-2.0f, 0, 0), new Vector3(-1.0f, 0, 0), Color.white);
        Debug.DrawRay(transform.position + new Vector3(0, 2.0f, 0), new Vector3(0, 1.0f, 0), Color.white);
        Debug.DrawRay(transform.position + new Vector3(2.0f, 0, 0), new Vector3(1.0f, 0, 0), Color.white);
        Debug.DrawRay(transform.position + new Vector3(0, -2.0f, 0), new Vector3(0, -1.0f, 0), Color.white);

        // 플레이어 오브젝트의 Collider2D min, center, max 위치 정보
        Bounds bounds = boxCollider2D.bounds;
        // 플레이어의 바닥 위치 설정
        underLeftPosition = new Vector2(bounds.min.x + 0.1f, bounds.min.y);
        underRightPosition = new Vector2(bounds.max.x - 0.1f, bounds.min.y - 0.5f);

        // 플레이어의 바닥 위치에 네모을 생성하고, 네모가 바닥과 닿아있으면 isGround = true
        isGround = Physics2D.OverlapArea(underLeftPosition, underRightPosition, groundLayer);

        if (PlayerBlock)
        {
            if (MovingPlayer)
            {
                rigid2D.velocity = new Vector2( 1 * MovingBlockSpeed, rigid2D.velocity.y);
            }
            else
            {
                rigid2D.velocity = new Vector2( 1 * MovingBlockSpeed, rigid2D.velocity.y);
            }
        }

        // 점프키를 누르고 있으면서 플레이어의 y속도가 0보다 크다면
        if (IsWater)
        {
            if (LongJump && rigid2D.velocity.y > 0)
            {
                rigid2D.gravityScale = 2.5f;
            }
            else
            {
                rigid2D.gravityScale = 4.5f;
            }
        }

        else
        {
            if (LongJump && rigid2D.velocity.y > 0)
            {
                rigid2D.gravityScale = 1.0f;
            }
            else
            {
                rigid2D.gravityScale = 2.5f;
            }
        }

        if (isGround == true && rigid2D.velocity.y <= 0)
        {
            isKnockBack = false;
            JumpCount = 1;
        }

        if (DashTime < 0)
        {
            speed = 5.0f;
            isdash = false;
        }

        else
        {
            DashTime -= Time.deltaTime;
            speed = dashSpeed;
            rigid2D.gravityScale = 0.0f;
            GetComponent<DashTimeBar>().DashBar.value = 0.0f;
        }

        dashWaitTime -= Time.deltaTime;

        if (GetKey == false)
        {
            Keyui = GameObject.Find("KeyUI");
            Keyui.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            Keyui.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (WarpCount == 0)
        {
            Warpui = GameObject.Find("WarpUI");
            Warpui.transform.GetChild(0).gameObject.SetActive(true);
            Warpui.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            Warpui.transform.GetChild(0).gameObject.SetActive(false);
            Warpui.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (playerHook.isCheck == false)
        {
            Hookui = GameObject.Find("HookUI");
            Hookui.transform.GetChild(0).gameObject.SetActive(true);
            Hookui.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            Hookui.transform.GetChild(0).gameObject.SetActive(false);
            Hookui.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            KnockBack(collision.transform.position);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingBlock"))
        {
            if (isGround == true)
            {
                var component = collision.gameObject.GetComponent<MovingBlock>(); // 충돌한 오브젝트의 MovingBlock 가져오기
                if (component.blockXY)
                {
                    PlayerBlock = true;
                    MovingPlayer = component.PlayerSame;
                    MovingBlockSpeed = component.speed;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterY"))
        {
            IsWater = true;
        }

        if (collision.CompareTag("WaterX"))
        {
            speed = 2;
        }

        if (collision.CompareTag("FinshGoal"))
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterY"))
        {
            IsWater = false;
        }
    }

    public void KnockBack(Vector2 Pos)
    {
        // 플레이어와 트랩의 좌우 위치
        float x = transform.position.x - Pos.x;
        if (x < 0)
            x = -1.0f;
        else
            x = 1.0f;

        isKnockBack = true;
        Vector2 dir = new Vector2(x, 1);
        int RanForce = Random.Range(3, 5);
        rigid2D.AddForce(dir * RanForce, ForceMode2D.Impulse);
    }

    public void Move(float x)
    {
        if (x != 0 || rigid2D.velocity.y != 0)
        {
            PlayerBlock = false;
        }
        
        if (playerHook.isCrash)
        {
            rigid2D.AddForce(new Vector2(x * speed, 0));
        }
        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }

    public void Jump()
    {
        if (JumpCount > 0)
        {
            GetComponent<Animator>().SetTrigger("isJumping");
            rigid2D.velocity = Vector2.up * jump;
            JumpCount = 0;
        }
    }

    public void dash()
    {
        if (dashWaitTime <= 0)
        {
            //audioSource.clip = audioClip;
            audioSource.PlayOneShot(audioClip);
            DashTime = 0.1f;
            dashWaitTime = 5.0f;
        }
    }

    public void Warp()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(-1.5f, 1.5f, 0), new Vector3(-1.0f, 1.0f, 0), 1.5f, LayerMask.GetMask("Ground"));
            if (!rayHit)
            {
                transform.position += new Vector3(-2, 2, 0);
                WarpCount = 0;
            }
        }

        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(-1.5f, -1.5f, 0), new Vector3(-1.0f, -1.0f, 0), 1.5f, LayerMask.GetMask("Ground")); ;
            if (!rayHit)
            {
                transform.position += new Vector3(-2, -2, 0);
                WarpCount = 0;
            }
        }

        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(1.5f, 1.5f, 0), new Vector3(1.0f, 1.0f, 0), 1.5f, LayerMask.GetMask("Ground"));
            if (!rayHit)
            {
                transform.position += new Vector3(2, 2, 0);
                WarpCount = 0;
            }
        }

        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(1.5f, -1.5f, 0), new Vector3(1.0f, -1.0f, 0), 1.5f, LayerMask.GetMask("Ground"));
            if (!rayHit)
            {
                transform.position += new Vector3(2, -2, 0);
                WarpCount = 0;
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(-2.0f, 0, 0), new Vector3(-1.0f, 0, 0), 1.5f, LayerMask.GetMask("Ground"));
            if (!rayHit)
            {
                transform.position += new Vector3(-2, 0, 0);
                WarpCount = 0;
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(2.0f, 0, 0), new Vector3(1.0f, 0, 0), 1.5f, LayerMask.GetMask("Ground"));
            if (!rayHit)
            {
                transform.position += new Vector3(2, 0, 0);
                WarpCount = 0;
            }
        }

        else if (Input.GetKey(KeyCode.S))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(0, -2.0f, 0), new Vector3(0, -1.0f, 0), 1.5f, LayerMask.GetMask("Ground"));
            if (!rayHit)
            {
                transform.position += new Vector3(0, -2, 0);
                WarpCount = 0;
            }
        }

        else if (Input.GetKey(KeyCode.W))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(0, 2.0f, 0), new Vector3(0, 1.0f, 0), 1.5f, LayerMask.GetMask("Ground"));
            if (!rayHit)
            {
                transform.position += new Vector3(0, 2, 0);
                WarpCount = 0;
            }
        }
        if (WarpCount == 0)
        {
            rigid2D.velocity = new Vector3(0, 0);
        }
    }

}

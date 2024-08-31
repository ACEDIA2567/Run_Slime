using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveMent : MonoBehaviour
{

    public float speed = 5.0f; // �̵� �ӵ�
    [SerializeField]
    private float jump = 8.0f;  // ���� �ӵ�
    [HideInInspector] // public ������ inspector view�� ������ �ʰ� ����
    public bool LongJump = false; // �� ������ ���� ����
    [HideInInspector]
    public bool isdash = false; // �뽬
    [SerializeField]
    private float dashSpeed = 40.0f;   // �뽬 �ӵ�
    private float DashTime;    // �뽬 �ð�
    [HideInInspector]
    public bool isKnockBack; // �˹� Ȯ��
    [HideInInspector]
    public float WarpCount = 0; // ���� Ƚ��
    [HideInInspector]
    public float dashWaitTime = 0.0f; // �뽬 ��Ÿ��
    [HideInInspector]
    public bool GetKey = false; // Ű ȹ��
    private bool IsWater = false; // �� �浹 üũ

    [HideInInspector]
    public Rigidbody2D rigid2D;
    public AudioClip audioClip;
    AudioSource audioSource;

    [SerializeField]
    private LayerMask groundLayer;       // �ٴ� üũ�� ���� ���̾�
    private BoxCollider2D boxCollider2D; // ������Ʈ�� �浹 ���� ������Ʈ
    public bool isGround;               // �ٴ� üũ (�ٴڿ� ��������� true)
    private Vector2 underLeftPosition;   // ���� �ٴ� ��ġ
    private Vector2 underRightPosition;  // ������ �ٴ� ��ġ

    private PlayerHook playerHook; // PlayerHook�� ����

    [HideInInspector]
    public Vector2 boxSize = new Vector2(1.1f, 0.3f); // �ٴڰ��� �浹�� ���� �ڽ�                  

    public int JumpCount = 0; // ���� ���� Ƚ��

    private bool PlayerBlock = false; // �÷��̾�� �����̴� ������ �浹 ����
    private bool MovingPlayer; // �����̴� ������ �¿� ��
    private float MovingBlockSpeed; // �����̴� ������ �ӵ���

    private GameObject Keyui; // Ű ������ UI
    private GameObject Warpui; // ���� ������ UI
    private GameObject Hookui; // ��ũ ������ UI

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        playerHook = GetComponent<PlayerHook>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position + new Vector3(-1.5f, 1.5f, 0), new Vector3(-1.0f, 1.0f, 0), Color.red); // ������
        Debug.DrawRay(transform.position + new Vector3(-1.5f, -1.5f, 0), new Vector3(-1.0f, -1.0f, 0), Color.blue); // ���ʾƷ�
        Debug.DrawRay(transform.position + new Vector3(1.5f, 1.5f, 0), new Vector3(1.0f, 1.0f, 0), Color.green); // ��������
        Debug.DrawRay(transform.position + new Vector3(1.5f, -1.5f, 0), new Vector3(1.0f, -1.0f, 0), Color.black); // �����ʾƷ�
        Debug.DrawRay(transform.position + new Vector3(-2.0f, 0, 0), new Vector3(-1.0f, 0, 0), Color.white);
        Debug.DrawRay(transform.position + new Vector3(0, 2.0f, 0), new Vector3(0, 1.0f, 0), Color.white);
        Debug.DrawRay(transform.position + new Vector3(2.0f, 0, 0), new Vector3(1.0f, 0, 0), Color.white);
        Debug.DrawRay(transform.position + new Vector3(0, -2.0f, 0), new Vector3(0, -1.0f, 0), Color.white);

        // �÷��̾� ������Ʈ�� Collider2D min, center, max ��ġ ����
        Bounds bounds = boxCollider2D.bounds;
        // �÷��̾��� �ٴ� ��ġ ����
        underLeftPosition = new Vector2(bounds.min.x + 0.1f, bounds.min.y);
        underRightPosition = new Vector2(bounds.max.x - 0.1f, bounds.min.y - 0.5f);

        // �÷��̾��� �ٴ� ��ġ�� �׸��� �����ϰ�, �׸� �ٴڰ� ��������� isGround = true
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

        // ����Ű�� ������ �����鼭 �÷��̾��� y�ӵ��� 0���� ũ�ٸ�
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
                var component = collision.gameObject.GetComponent<MovingBlock>(); // �浹�� ������Ʈ�� MovingBlock ��������
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
        // �÷��̾�� Ʈ���� �¿� ��ġ
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

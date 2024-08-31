using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public ParticleSystem MoveParticle; // 움직일때의 파티클
    private MoveMent movement;
    private PlayerHook playerHook;
    [HideInInspector]
    public float x; // 플레이어의 이동 여부

    private bool ItemCheckSound; // 아이템 획득을 알리는 사운드 체크

    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private AudioSource WarpaudioSource;
    private void Awake()
    {
        movement = GetComponent<MoveMent>();
        playerHook = GetComponent<PlayerHook>();
        audioSource = GetComponent<AudioSource>();
        WarpaudioSource = GetComponent<AudioSource>();
        ItemCheckSound = false;
    }

    private void Update()
    {
        // 플레이어가 함정에 충돌 했다면 모든 이동 가능 키를 사용 할 수 없게 설정
        if (movement.isKnockBack == false)
        {
            x = Input.GetAxisRaw("Horizontal");
            movement.Move(x);

            if (Input.GetKey(KeyCode.A))
            {
                if (movement.isGround == true)
                {
                    MoveParticle.Play();
                }
                transform.rotation = Quaternion.Euler(0, false ? 0 : 180, 0);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                if (movement.isGround == true)
                {
                    MoveParticle.Play();
                }
                transform.rotation = Quaternion.Euler(0, true ? 0 : 180, 0);
            }

            // 플레이어 점프
            if (Input.GetKeyDown(KeyCode.W))
            {
                movement.Jump();
            }

            // 플레이어가 점프 키는 누르고 있다면
            if (Input.GetKey(KeyCode.W))
            {
                movement.LongJump = true;
            }

            // 플레이어가 점프 키를 떼었다면
            else if (Input.GetKeyUp(KeyCode.W))
            {
                movement.LongJump = false;
            }

            // 플레이어가 특수키인 스페이스와 화살표를 눌렀다면
            if (Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    movement.dash();
            }

            // 플레이어가 워프 아이템인 마우스 우클릭을 눌렀을 때
            if (Input.GetMouseButtonDown(1))
            {
                if (movement.WarpCount > 0)
                {
                    movement.Warp();
                    WarpaudioSource.clip = audioClips[3];
                    WarpaudioSource.Play();
                }
            }
        }

        if (ItemCheckSound == true)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            ItemCheckSound = false;
        }

    }

    // 플레이어가 아이템 태그와 충돌을 했다면
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 점프 아이템
        if (collision.CompareTag("JumpItem"))
        {
            movement.JumpCount += 1;
            Destroy(collision.gameObject);
            ItemCheckSound = true;
        }

        // 워프 아이템
        if (collision.CompareTag("WarpItem"))
        {
            if (movement.WarpCount == 0)
            {
                movement.WarpCount += 1;
            }
            Destroy(collision.gameObject);
            ItemCheckSound = true;
        }

        // 플레이어가 키를 획득 했다면
        if (collision.CompareTag("Key"))
        {
            movement.GetKey = true;
            Destroy(collision.gameObject);
            audioSource.clip = audioClips[4];
            audioSource.Play();
        }

        // 후크 아이템
        if (collision.CompareTag("HookItem"))
        {
            playerHook.isCheck = true;
            Destroy(collision.gameObject);
            ItemCheckSound = true;
        }

        if (collision.CompareTag("ItemSpowner"))
        {
            var component = collision.gameObject.GetComponent<ItemSpawner>();
            component.ItemCheck = true;
        }
    }

    // 플레이어가 열쇠를 획득 후 문에 도착했다면
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            if (movement.GetKey == true)
            {
                Destroy(collision.gameObject);
                movement.GetKey = false;
                audioSource.clip = audioClips[1];
                audioSource.Play();
            }
            else
            {
                audioSource.clip = audioClips[2];
                audioSource.Play();
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public ParticleSystem MoveParticle; // �����϶��� ��ƼŬ
    private MoveMent movement;
    private PlayerHook playerHook;
    [HideInInspector]
    public float x; // �÷��̾��� �̵� ����

    private bool ItemCheckSound; // ������ ȹ���� �˸��� ���� üũ

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
        // �÷��̾ ������ �浹 �ߴٸ� ��� �̵� ���� Ű�� ��� �� �� ���� ����
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

            // �÷��̾� ����
            if (Input.GetKeyDown(KeyCode.W))
            {
                movement.Jump();
            }

            // �÷��̾ ���� Ű�� ������ �ִٸ�
            if (Input.GetKey(KeyCode.W))
            {
                movement.LongJump = true;
            }

            // �÷��̾ ���� Ű�� �����ٸ�
            else if (Input.GetKeyUp(KeyCode.W))
            {
                movement.LongJump = false;
            }

            // �÷��̾ Ư��Ű�� �����̽��� ȭ��ǥ�� �����ٸ�
            if (Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    movement.dash();
            }

            // �÷��̾ ���� �������� ���콺 ��Ŭ���� ������ ��
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

    // �÷��̾ ������ �±׿� �浹�� �ߴٸ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ������
        if (collision.CompareTag("JumpItem"))
        {
            movement.JumpCount += 1;
            Destroy(collision.gameObject);
            ItemCheckSound = true;
        }

        // ���� ������
        if (collision.CompareTag("WarpItem"))
        {
            if (movement.WarpCount == 0)
            {
                movement.WarpCount += 1;
            }
            Destroy(collision.gameObject);
            ItemCheckSound = true;
        }

        // �÷��̾ Ű�� ȹ�� �ߴٸ�
        if (collision.CompareTag("Key"))
        {
            movement.GetKey = true;
            Destroy(collision.gameObject);
            audioSource.clip = audioClips[4];
            audioSource.Play();
        }

        // ��ũ ������
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

    // �÷��̾ ���踦 ȹ�� �� ���� �����ߴٸ�
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

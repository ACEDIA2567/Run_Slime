using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    public LineRenderer line; // ��ũ�� �÷��̾���� ���� ����
    public Transform hook; // ��ũ�� ��ġ ����
    Vector2 mousedir; // ���콺�� �÷��̾���� �Ÿ�

    [HideInInspector]
    public bool isHookActive; // ��ũ�� ��� ����
    [HideInInspector]
    public bool isLineMax; // ��ũ�� �÷��̾��� �Ÿ��� �ִ� �϶��� ����
    [HideInInspector]
    public bool isCrash; // �浹 ����
    [HideInInspector]
    public bool isCheck = false; // ��ũ ������ ȹ�� ����


    public AudioClip HookSound; // Hook �Ҹ� ����
    private AudioSource PlayHookSound; // Hook �Ҹ� ����

    private void Start()
    {
        PlayHookSound = GetComponent<AudioSource>();
        line.positionCount = 2; // ������ ������ ���� = ù �κ�, �� �κ�
        line.endWidth = line.startWidth = 0.05f; // ������ ����
        line.SetPosition(0, transform.position); // ù ���� ��ġ = �÷��̾��� ��ġ
        line.SetPosition(1, hook.position); // �� ���� ��ġ = Hook�� ��ġ
        line.useWorldSpace = true;
        isCrash = true;
    }

    private void Update()
    {
        line.SetPosition(0, transform.position); // ù ���� ��ġ = �÷��̾��� ��ġ
        line.SetPosition(1, hook.position); // �� ���� ��ġ = Hook�� ��ġ

        // ���� �������� ��� ������ �� (ȹ�� ��)�� ��
        if (isCheck)
        {
            if (Input.GetMouseButtonDown(0) && !isHookActive)
            {
                hook.position = transform.position;
                // ���콺�� �÷��̾���� ��ǥ�� �Ÿ��� ��
                mousedir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                isHookActive = true;
                hook.gameObject.SetActive(true);
                isCheck = false;
            }
        }

        // ������ ���󰡰� ���� ��
        if (isHookActive && !isLineMax && !isCrash)
        {
            // ��ũ ���� ���
            PlayHookSound.clip = HookSound;
            PlayHookSound.Play();
            // ��ũ�� ��ġ�� ���콺Ŀ�� ��ġ�� �̵�
            hook.Translate(mousedir.normalized * Time.deltaTime * 15);

            // ��ũ�� �÷��̾ƿ��� �Ÿ��� 3���� ũ�ٸ�
            if (Vector2.Distance(transform.position, hook.position) > 3)
            {
                isLineMax = true;
            }
        } 
        // ���� �Ÿ��� �ִ� �Ÿ��� �Ǿ��� ��
        else if(isHookActive && isLineMax && !isCrash)
        {
            // ��ũ�� ��ġ�� �÷��̾� ��ġ�� �̵�
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 15);
            if (Vector2.Distance(transform.position, hook.position) < 0.1f)
            {
                isHookActive = false;
                isLineMax = false;
                hook.gameObject.SetActive(false);
            }
        }
        // ������ ��ũ�� ��Ҵٸ�
        else if (isCrash)
        {
            // ���콺 Ŭ�� �� ���� ���
            if (Input.GetMouseButtonDown(0))
            {
                isHookActive = false;
                isLineMax = false;
                isCrash = false;
                hook.GetComponent<CrashHook>().joint2D.enabled = false;
                hook.gameObject.SetActive(false);
            }
        }
    }
}

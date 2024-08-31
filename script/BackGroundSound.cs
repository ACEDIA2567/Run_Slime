using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSound : MonoBehaviour
{
    AudioSource BackSound; // �������
    public AudioClip[] ManyBackSound; // 
    public GameObject playerPoint; // �÷��̾� ��ġ
    [HideInInspector]
    public float SoundCheck; // ��ġ�� ���� ����

    private void Start()
    {
        BackSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SoundCheck == 0)
        {
            if (BackSound.clip != ManyBackSound[0])
            {
                BackSound.clip = ManyBackSound[0];
                BackSound.loop = true;
                BackSound.Play();
            }
        }
        else if (SoundCheck == 1)
        {
            if (BackSound.clip != ManyBackSound[1])
            {
                BackSound.clip = ManyBackSound[1];
                BackSound.loop = true;
                BackSound.Play();
            }
        }
        else if (SoundCheck == 2)
        {
            if (BackSound.clip != ManyBackSound[2])
            {
                BackSound.clip = ManyBackSound[2];
                BackSound.loop = true;
                BackSound.Play();
            }
        }
        else
        {
            if (BackSound.clip != ManyBackSound[3])
            {
                BackSound.clip = ManyBackSound[3];
                BackSound.loop = true;
                BackSound.Play();
            }
        }

        if (playerPoint.transform.position.y > 55.5 && playerPoint.transform.position.y < 97)
        {
            if (playerPoint.transform.position.x > 21 && playerPoint.transform.position.x < 79.5)
            {
                SoundCheck = 2;
            }
            else
            {
                SoundCheck = 3;
            }
        }
        else if (playerPoint.transform.position.y > 18.5 && playerPoint.transform.position.y < 55.5)
        {
            SoundCheck = 0;
        }
        else if (playerPoint.transform.position.y > -7.0 && playerPoint.transform.position.y < 18.5)
        {
            SoundCheck = 1;
        }
    }


}

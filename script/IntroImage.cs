using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroImage : MonoBehaviour
{
    public Image image;
    private Color color;

    private void Start()
    {
        Image image = GetComponent<Image>();
        color.a = 0.0f;
        image.color = color;
    }

    void Update()
    {
        if (image.color.a >= 1)
        {
            GameObject.Find("Canvas").transform.Find("GameStart").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("GameQuit").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("NameText").gameObject.SetActive(true);
        }
        else
        {
            // �����Ӹ��� a�� ���� ��Ŵ
            color.a += 0.0005f;
            color.r = 255;
            color.b = 255;
            color.g = 255;
            image.color = color;
        }

        // �ٸ� Ű�� ���콺 Ŭ���� �ٷ� ����
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            color.a = 1.0f;
            image.color = color;
        }

    }
}

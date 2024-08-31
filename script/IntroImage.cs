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
            // 프레임마다 a를 증가 시킴
            color.a += 0.0005f;
            color.r = 255;
            color.b = 255;
            color.g = 255;
            image.color = color;
        }

        // 다른 키나 마우스 클릭시 바로 변경
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            color.a = 1.0f;
            image.color = color;
        }

    }
}

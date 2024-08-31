using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashTimeBar : MonoBehaviour
{
    public Slider DashBar;
    public Image Fill;

    private void Awake()
    {
        // 대쉬바의 최대 값을 최대 대쉬 재사용시간으로 설정
        //DashBar.maxValue = 5.0f;
    }

    private void Update()
    {
        DashBar.value += Time.deltaTime;
        if (DashBar.value == 5.0f)
        {
            Fill.color = Color.green;
        }
        else
        {
            Fill.color = Color.red;
        }
    }

}

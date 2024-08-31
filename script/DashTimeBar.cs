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
        // �뽬���� �ִ� ���� �ִ� �뽬 ����ð����� ����
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

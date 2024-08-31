using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillColor : MonoBehaviour
{
    public Image Fill;

    private void Awake()
    {
        Fill.color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCoolTime : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.down * 35.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;
    [SerializeField]
    private GameObject SlimePlayer; // �÷��̾� �ڽ�

    private void Update()
    {
        Setup(SlimePlayer.transform);
    }

    public void Setup(Transform Player)
    {
        // Slider UI�� �Ѿƴٴ� Ÿ���� ����
        targetTransform = Player;
        // RectTransform ������Ʈ ���� ������
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // ������Ʈ�� ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ ���� ����
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        // ȭ�鳻���� ��ǥ + distance��ŭ ������ ��ġ�� Slider UI�� ��ġ�� ����
        rectTransform.position = screenPosition + distance;
    }

}

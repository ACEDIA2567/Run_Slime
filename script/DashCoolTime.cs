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
    private GameObject SlimePlayer; // 플레이어 자신

    private void Update()
    {
        Setup(SlimePlayer.transform);
    }

    public void Setup(Transform Player)
    {
        // Slider UI가 쫓아다닐 타겟을 설정
        targetTransform = Player;
        // RectTransform 컴포넌트 정보 얻어오기
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // 오브젝트의 월드 좌표를 기준으로 화면에서의 좌표 값을 구함
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        // 화면내에서 좌표 + distance만큼 떨어진 위치를 Slider UI의 위치로 설정
        rectTransform.position = screenPosition + distance;
    }

}

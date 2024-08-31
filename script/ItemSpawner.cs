using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Item; // 스폰할 아이템 정보
    [SerializeField]
    private Transform spawnPoint; // 스폰 위치 정보
    [HideInInspector]
    public bool ItemCheck; // 아이템 삭제 여부
    private float SpawnTime = 0.0f; // 아이템 스폰 시간

    private void Start()
    {
        ItemCheck = true;
    }

    private void Update()
    {
        if (ItemCheck)
        {
            // 생성 시간이 감소하고 0미만이 되면
            SpawnTime -= Time.deltaTime;
            if (SpawnTime < 0.0f)
            {
                // 아이템 생성
                GameObject NewItem = Instantiate(Item, spawnPoint.position, Quaternion.identity);
                SpawnTime = 5.0f;
                ItemCheck = false;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Item; // ������ ������ ����
    [SerializeField]
    private Transform spawnPoint; // ���� ��ġ ����
    [HideInInspector]
    public bool ItemCheck; // ������ ���� ����
    private float SpawnTime = 0.0f; // ������ ���� �ð�

    private void Start()
    {
        ItemCheck = true;
    }

    private void Update()
    {
        if (ItemCheck)
        {
            // ���� �ð��� �����ϰ� 0�̸��� �Ǹ�
            SpawnTime -= Time.deltaTime;
            if (SpawnTime < 0.0f)
            {
                // ������ ����
                GameObject NewItem = Instantiate(Item, spawnPoint.position, Quaternion.identity);
                SpawnTime = 5.0f;
                ItemCheck = false;
            }
        }
    }

}

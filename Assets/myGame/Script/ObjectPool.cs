using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject slimePrefab; // Ǯ���� ������Ʈ�� ������
    public GameObject sansPrefab;

    GameObject prefab;

    List<GameObject> Pool;
    public List<GameObject> slimePool { get; private set; } // �����Ǵ� ������Ʈ�� ����Ʈ
    public List<GameObject> sansPool { get; private set; }

    private void Awake()
    {
        Instance = this;

        Pool = new List<GameObject>();
        slimePool = new List<GameObject>();
        sansPool = new List<GameObject>();

        for (var i = 0; i < 50; i++)
        {
            var obj = Instantiate(slimePrefab, transform);
            obj.SetActive(false);
            slimePool.Add(obj);
        }
        for (var i = 0; i < 30; i++)
        {
            var obj = Instantiate(sansPrefab, transform);
            obj.SetActive(false);
            sansPool.Add(obj);
        }
    }

    

    // ������Ʈ Ǯ���� �����ϴ� ������Ʈ�� ��ȯ�Ѵ�.
    public GameObject GetObject(string name)
    {
        switch (name)
        {
            case "Slime":
                Pool = slimePool;
                prefab = slimePrefab;
                break;
            case "Sans":
                Pool = sansPool;
                prefab = sansPrefab;
                break;
        }

        // Ǯ���� ��Ȱ��ȭ�� ������Ʈ�� ã�� ��ȯ�Ѵ�.
        foreach (var obj in Pool)
            if (!obj.activeSelf)
            {
                return obj;
            }

        // ��Ȱ��ȭ�� ������Ʈ�� ���� ���, Ǯ�� Ȯ���Ѵ�..
        var newObj = Instantiate(prefab, transform);
        Pool.Add(newObj);
        //poolCount++;
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}

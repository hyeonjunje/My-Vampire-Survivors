using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject prefab; // Ǯ���� ������Ʈ�� ������

    [SerializeField] private int poolCount;     // Ǯ���� ������Ʈ�� �ִ� ����


    public List<GameObject> Pool { get; private set; } // �����Ǵ� ������Ʈ�� ����Ʈ

    private void Awake()
    {
        Instance = this;
        Pool = new List<GameObject>();

        for(var i = 0; i < poolCount; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            Pool.Add(obj);
        }
    }

    

    // ������Ʈ Ǯ���� �����ϴ� ������Ʈ�� ��ȯ�Ѵ�.
    public GameObject GetObject()
    {
        // Ǯ���� ��Ȱ��ȭ�� ������Ʈ�� ã�� ��ȯ�Ѵ�.
        foreach (var obj in Pool)
            if (!obj.activeSelf)
            {
                return obj;
            }

        // ��Ȱ��ȭ�� ������Ʈ�� ���� ���, Ǯ�� Ȯ���Ѵ�..
        var newObj = Instantiate(prefab, transform);
        Pool.Add(newObj);
        poolCount++;
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}

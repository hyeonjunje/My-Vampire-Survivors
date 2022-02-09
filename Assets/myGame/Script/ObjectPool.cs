using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject prefab; // 풀링할 오브젝트의 프리팹

    [SerializeField] private int poolCount;     // 풀링할 오브젝트의 최대 개수


    public List<GameObject> Pool { get; private set; } // 관리되는 오브젝트의 리스트

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

    

    // 오브젝트 풀에서 관리하는 오브젝트를 반환한다.
    public GameObject GetObject()
    {
        // 풀에서 비활성화된 오브젝트를 찾아 반환한다.
        foreach (var obj in Pool)
            if (!obj.activeSelf)
            {
                return obj;
            }

        // 비활성화된 오브젝트가 없을 경우, 풀을 확장한다..
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

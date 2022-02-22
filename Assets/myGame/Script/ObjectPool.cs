using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject slimePrefab; // 풀링할 오브젝트의 프리팹
    public GameObject sansPrefab;

    GameObject prefab;

    List<GameObject> Pool;
    public List<GameObject> slimePool { get; private set; } // 관리되는 오브젝트의 리스트
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

    

    // 오브젝트 풀에서 관리하는 오브젝트를 반환한다.
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

        // 풀에서 비활성화된 오브젝트를 찾아 반환한다.
        foreach (var obj in Pool)
            if (!obj.activeSelf)
            {
                return obj;
            }

        // 비활성화된 오브젝트가 없을 경우, 풀을 확장한다..
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

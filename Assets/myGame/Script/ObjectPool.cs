using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    // ��
    public GameObject slimePrefab; // Ǯ���� ������Ʈ�� ������
    public GameObject sansPrefab;

    // �ɷ�
    public GameObject doodlePrefab;
    public GameObject whipPrefab;

    // ������
    public GameObject redSoulPrefab;
    public GameObject blueSoulPrefab;

    // ȿ��
    public GameObject dmgTextPrefab;

    GameObject prefab;

    List<GameObject> Pool;
    public List<GameObject> slimePool { get; private set; } // �����Ǵ� ������Ʈ�� ����Ʈ
    public List<GameObject> sansPool { get; private set; }
    public List<GameObject> doodlePool { get; private set; }
    public List<GameObject> redSoulPool { get; private set; }
    public List<GameObject> blueSoulPool { get; private set; }
    public List<GameObject> dmgTextPool { get; private set; }
    public List<GameObject> whipPool { get; private set; }
    private void Awake()
    {
        Instance = this;

        Pool = new List<GameObject>();
        slimePool = new List<GameObject>();
        sansPool = new List<GameObject>();
        doodlePool = new List<GameObject>();
        redSoulPool = new List<GameObject>();
        blueSoulPool = new List<GameObject>();
        dmgTextPool = new List<GameObject>();
        whipPool = new List<GameObject>();

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
        for (var i = 0; i < 10; i++)
        {
            var obj = Instantiate(doodlePrefab, transform);
            obj.SetActive(false);
            doodlePool.Add(obj);
        }
        for (var i = 0; i < 10; i++)
        {
            var obj = Instantiate(whipPrefab, transform);
            obj.SetActive(false);
            whipPool.Add(obj);
        }
        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(redSoulPrefab, transform);
            obj.SetActive(false);
            redSoulPool.Add(obj);
        }
        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(blueSoulPrefab, transform);
            obj.SetActive(false);
            blueSoulPool.Add(obj);
        }
        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(dmgTextPrefab, transform);
            obj.SetActive(false);
            dmgTextPool.Add(obj);
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
            case "Doodle":
                Pool = doodlePool;
                prefab = doodlePrefab;
                break;
            case "Whip":
                Pool = whipPool;
                prefab = whipPrefab;
                break;
            case "RedSoul":
                Pool = redSoulPool;
                prefab = redSoulPrefab;
                break;
            case "BlueSoul":
                Pool = blueSoulPool;
                prefab = blueSoulPrefab;
                break;
            case "DmgText":
                Pool = dmgTextPool;
                prefab = dmgTextPrefab;
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

    public void AllReturnObject()
    {
        int iCount = transform.childCount;
        for(int i = 0; i < iCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.activeSelf) // Ȱ��ȭ�Ǿ� ������ ���� ��Ȱ��ȭ
            {
                child.SetActive(false);
            }
        }
    }
}

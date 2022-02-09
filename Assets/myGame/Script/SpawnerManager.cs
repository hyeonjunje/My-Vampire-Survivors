using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �� : characterAxis ������Ʈ�� �ڽ�����
public class SpawnerManager : MonoBehaviour
{
    public float radius = 80.0f;

    
    private int cnt = 0;
    private float timer = 0.0f;


    [SerializeField] private Transform character;
    [SerializeField] private GameObject enemy;

    private void Awake()
    {
        ObjectPool.Instance.prefab = enemy;  // enemy �������� ����� ������ƮǮ ���
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5.0f)
        {
            Spawn();
            timer = 0;
            cnt++;
        }
    }

    private void Spawn()
    {
        for(var i = 0; i < cnt * 10; i++)
        {
            var obj = ObjectPool.Instance.GetObject();
            obj.transform.position = randPos();
            obj.SetActive(true);

            //Instantiate(enemy, randPos(), Quaternion.identity);
        }
    }

    private Vector3 randPos()
    {
        var randAngle = Random.Range(0, 359) * Mathf.Deg2Rad;
        float posX = character.position.x + Mathf.Cos(randAngle) * radius;
        float posy = character.position.y + Mathf.Sin(randAngle) * radius;

        return new Vector3(posX, posy, 0);
    }
}

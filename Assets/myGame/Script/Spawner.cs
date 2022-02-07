using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int cnt = 0;
    private float timer = 0.0f;
    [SerializeField] private GameObject enemy;
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5.0f)
        {
            Spawn();
            timer = 0;
            cnt++;
        }
    }

    private void Spawn()
    {
        int randint = Random.Range(0, 20);
        if (cnt >= 1)
        { 
            //Debug.Log("초 경과   랜덤 수 :" + randint);
            if(randint < 5) Instantiate(enemy, transform.position, Quaternion.identity);
        }
        else if (cnt == 20)
        {
            //Debug.Log("20초 경과   랜덤 수 :" + randint);
            if (randint < 5) Instantiate(enemy, transform.position, Quaternion.identity);
        }
        else if (cnt == 30)
        {
            //Debug.Log("30초 경과   랜덤 수 :" + randint);
            if (randint < 10) Instantiate(enemy, transform.position, Quaternion.identity);
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 붙일 곳 : characterAxis 오브젝트의 자식으로
public class SpawnerManager : MonoBehaviour
{
    public float radius = 80.0f;

    [SerializeField] private GameObject spawner;
    //private List<GameObject> spawners = new List<GameObject>();
    private void Awake()
    {
        for(int i = 0; i < 360; i += 2)
        {
            Vector3 pos = transform.position + new Vector3(Mathf.Cos(i * Mathf.PI / 180) * radius, Mathf.Sin(i * Mathf.PI / 180) * radius, 0);
            Instantiate(spawner, pos, Quaternion.identity).transform.parent = transform;
        }
    }
}

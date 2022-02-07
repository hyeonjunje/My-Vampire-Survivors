using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject HpBar;
    public GameObject canvas;
    public float height = 1.0f;
    RectTransform hpBar;

    public float speed = 5.0f;

    //public GameObject target;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate((new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0) - transform.position).normalized * Time.deltaTime * speed);
    }
}

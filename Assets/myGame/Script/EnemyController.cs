using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5.0f;
    void Update()
    {
        transform.Translate((new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0) - transform.position).normalized * Time.deltaTime * speed);
    }
}

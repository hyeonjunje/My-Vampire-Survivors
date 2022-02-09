using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    [SerializeField] private EnemyData enemyData;
    void Update()
    {
        Vector2 movement = new Vector2(Camera.main.transform.position.x - transform.position.x,
            Camera.main.transform.position.y - transform.position.y);
        rb.velocity = movement.normalized * enemyData.MoveSpeed;


        // 예전 transform 으로 이동 구현할 때 코드
        /*transform.Translate((new Vector3(Camera.main.transform.position.x, 
            Camera.main.transform.position.y, 0) - transform.position).normalized * Time.deltaTime * enemyData.MoveSpeed);*/
    }
}

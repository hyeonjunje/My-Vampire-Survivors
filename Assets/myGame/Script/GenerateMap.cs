using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public Transform grid;         // 캐릭터에 따라 움직일 맵
    public Transform character;    // 캐릭터

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("character"))
        {
            //Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

            float angle = Mathf.Atan2(character.position.y - transform.position.y, character.position.x - transform.position.x) * Mathf.Rad2Deg;

            Debug.Log("각도는" + angle);

            if (43.5 <= angle && angle < 46.5) // 오른쪽, 위로 이동
            {
                grid.position += new Vector3(100, 100, 0);
            }
            else if (133.5 <= angle && angle < 136.5) // 왼쪽, 위로 이동
            {
                grid.position += new Vector3(-100, 100, 0);
            }
            else if (-136.5 < angle && angle <= -133.5) // 왼쪽, 아래로 이동
            {
                grid.position += new Vector3(-100, -100, 0);
            }
            else if (-46.5 < angle && angle <= -43.5) // 오른쪽, 위로 이동
            {
                grid.position += new Vector3(100, -100, 0);
            }

            else if (45 < angle && angle < 135) // 위로 이동
            {
                grid.position += new Vector3(0, 100, 0);
            }
            else if (-135 < angle && angle < -45) // 아래로 이동
            {
                grid.position += new Vector3(0, -100, 0);
            }
            else if (135 < angle && angle <= 180 || -180 <= angle && angle < -135) // 왼쪽으로 이동
            {
                grid.position += new Vector3(-100, 0, 0);
            }
            else if (-45 < angle && angle < 45) // 오른쪽으로 이동
            {
                grid.position += new Vector3(100, 0, 0);
            }
        }
    }
}

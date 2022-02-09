using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public Transform grid;         // ĳ���Ϳ� ���� ������ ��
    public Transform character;    // ĳ����

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("character"))
        {
            //Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

            float angle = Mathf.Atan2(character.position.y - transform.position.y, character.position.x - transform.position.x) * Mathf.Rad2Deg;

            Debug.Log("������" + angle);

            if (43.5 <= angle && angle < 46.5) // ������, ���� �̵�
            {
                grid.position += new Vector3(100, 100, 0);
            }
            else if (133.5 <= angle && angle < 136.5) // ����, ���� �̵�
            {
                grid.position += new Vector3(-100, 100, 0);
            }
            else if (-136.5 < angle && angle <= -133.5) // ����, �Ʒ��� �̵�
            {
                grid.position += new Vector3(-100, -100, 0);
            }
            else if (-46.5 < angle && angle <= -43.5) // ������, ���� �̵�
            {
                grid.position += new Vector3(100, -100, 0);
            }

            else if (45 < angle && angle < 135) // ���� �̵�
            {
                grid.position += new Vector3(0, 100, 0);
            }
            else if (-135 < angle && angle < -45) // �Ʒ��� �̵�
            {
                grid.position += new Vector3(0, -100, 0);
            }
            else if (135 < angle && angle <= 180 || -180 <= angle && angle < -135) // �������� �̵�
            {
                grid.position += new Vector3(-100, 0, 0);
            }
            else if (-45 < angle && angle < 45) // ���������� �̵�
            {
                grid.position += new Vector3(100, 0, 0);
            }
        }
    }
}

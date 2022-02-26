using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    

    public float soulAmount;
    public float getRange;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "character")
        {
            Vector3 dir = (collision.transform.transform.position - gameObject.transform.position).normalized;

            if ((gameObject.transform.position - collision.transform.transform.position).magnitude > getRange)
            {
                gameObject.transform.Translate(dir * Time.deltaTime * 30.0f);  // 플레이어 속도보다는 빨라야 함 
            }
            else
            {
                Player player = collision.GetComponentInParent<Player>();
                player.levelUp(soulAmount);

                // 오브젝트 풀로 돌아감
                ObjectPool.Instance.ReturnObject(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public int damage;
    public float coolTime;
    public string abilityName;

    public Transform player; // �÷��̾��� ��ġ

    SpriteRenderer spriteRenderer;
    Animator anim;

    // Doodle
    bool isFilp;
    public bool isCrash;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); 
    }

    Vector2 fireVec()
    {
        // �÷��̾� �ֺ� ���� ����� �������� ���� ��ȯ 

        RaycastHit2D[] rayHit = Physics2D.CircleCastAll(player.position, 20f, Vector2.zero, 0f, LayerMask.GetMask("Monster"));
        if (rayHit != null)
        {
            Vector3 curVec = player.position;
            float distance = Mathf.Infinity;
            Vector2 vec = Vector2.zero;
            foreach (var obj in rayHit)
            {
                if (distance > (obj.collider.transform.position - curVec).magnitude)
                {
                    distance = (obj.collider.transform.position - curVec).magnitude;
                    vec = obj.collider.transform.position - player.position;
                }
            }

            return vec.normalized;
        }

        return Vector2.zero;
    }

    void OnEnable()
    {
       
    }

    public void logic()
    {
        Debug.Log("¹��!!!!");
        switch (abilityName)
        {
            case "Doodle":
                Vector2 vec = fireVec();
                if (vec == Vector2.zero) { ObjectPool.Instance.ReturnObject(gameObject); return; }  // ������ ���� ������ �׳� ������

                transform.position = player.position;
                if (vec.x < 0) { isFilp = true; spriteRenderer.flipX = false; }

                Rigidbody2D rigid = GetComponent<Rigidbody2D>();
                rigid.AddForce(vec * 15, ForceMode2D.Impulse);
                Debug.Log("�߻�");
                StartCoroutine(doodleAttack());
                break;
        }
    }
    

    IEnumerator doodleAttack()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(0.6f);
        rigid.velocity = Vector2.zero;
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(3.0f);
        anim.SetBool("isAttack", false);
        

        if (isFilp) { isFilp = false; spriteRenderer.flipX = true; }

        ObjectPool.Instance.ReturnObject(gameObject);
    }
}

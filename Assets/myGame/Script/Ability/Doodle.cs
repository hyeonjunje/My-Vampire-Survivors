using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodle : Ability
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    
    // Doodle
    bool isFilp;

    void Awake()
    {
        transform.localScale = new Vector3(10, 10, 0);
        damage = 7.0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        
    }

    Vector2 fireVec()
    {
        // 플레이어 주변 가장 가까운 적으로의 방향 반환 

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

    public void logic()
    {
        Vector2 vec = fireVec();
        if (vec == Vector2.zero) { ObjectPool.Instance.ReturnObject(gameObject); return; }  // 주위의 적이 없으면 그냥 없어짐

        transform.position = player.position;
        if (vec.x < 0) { isFilp = true; spriteRenderer.flipX = false; }

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(vec * 15, ForceMode2D.Impulse);
        Debug.Log("발사");
        StartCoroutine(doodleAttack());
        
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

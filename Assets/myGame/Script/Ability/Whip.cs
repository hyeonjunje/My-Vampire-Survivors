using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : Ability
{
    bool isMove;
    bool isFilp;

    SpriteRenderer spriteRenderer;


    void Awake()
    {
        transform.localScale = new Vector3(30, 20, 0);
        damage = 5.0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        
    }

    public void logic()
    {
        Transform playerLogic = player.transform.GetChild(0);
        if (playerLogic.rotation.y == 0) isFilp = true;
        else isFilp = false;

        StartCoroutine(WhipAttack());
    }
    IEnumerator WhipAttack()
    {
        if (isFilp)
        {
            transform.position = player.position + Vector3.right * 5;
            spriteRenderer.flipX = true;
            yield return new WaitForSeconds(0.1f);
            transform.position = player.position + Vector3.left * 5;
            spriteRenderer.flipX = false;
        }
        else
        {
            transform.position = player.position + Vector3.left * 5;
            spriteRenderer.flipX = false;
            yield return new WaitForSeconds(0.1f);
            transform.position = player.position + Vector3.right * 5;
            spriteRenderer.flipX = true;
        }

        yield return new WaitForSeconds(0.1f);
        ObjectPool.Instance.ReturnObject(gameObject);
    }
}

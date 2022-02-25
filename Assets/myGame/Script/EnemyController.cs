using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    int curHp;
    float doodleAttackTime;

    public GameObject player;
    public GameObject canvas;
    public Image hpBar;

    bool isHit;

    // 오브젝트 풀에 의해 다시 활성화되면 다 초기화 해줘야 함
    void OnEnable()    
    {
        curHp = enemyData.Hp;
        hpBar.fillAmount = (float)curHp / enemyData.Hp;
        canvas.SetActive(false);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if(!isHit) rb.velocity = Vector2.zero;
        Move();
    }

    void Move()
    {
        Vector3 movement = (player.transform.position - transform.position).normalized;
        transform.Translate(movement * enemyData.MoveSpeed * Time.deltaTime);   
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ability Multi Hit")
        {
            int abilityDamage = collision.GetComponent<Ability>().damage;

            StopCoroutine(hideHp());
            StartCoroutine(hideHp());
            canvas.SetActive(true);

            Vector2 reactVec = new Vector2(transform.position.x - collision.transform.position.x,
            transform.position.y - collision.transform.position.y).normalized;

            doodleAttackTime += Time.deltaTime;
            if(doodleAttackTime > 0.5f)
            {
                StartCoroutine(hit(reactVec, abilityDamage));
                doodleAttackTime = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ability")
        {
            int abilityDamage = collision.GetComponent<Ability>().damage;

            StopCoroutine(hideHp());
            StartCoroutine(hideHp());
            canvas.SetActive(true);

            Vector2 reactVec = new Vector2(transform.position.x - collision.transform.position.x,
            transform.position.y - collision.transform.position.y).normalized;

            StartCoroutine(hit(reactVec, abilityDamage));
        }
    }
    
    

    IEnumerator hideHp()
    {
        yield return new WaitForSeconds(10.0f);

        canvas.SetActive(false);
    }

    IEnumerator hit(Vector2 reactVec, int damage)
    {
        //curHp -= player.GetComponent<Player>().playerDamage;
        curHp -= damage;
        hpBar.fillAmount = (float)curHp / enemyData.Hp;

        if (curHp > 0)
        {
            isHit = true;
            rb.AddForce(reactVec * 5f, ForceMode2D.Impulse);
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            isHit = false;

        }
        else
        {
            var obj = ObjectPool.Instance.GetObject("BlueSoul");
            obj.transform.position = gameObject.transform.position;
            obj.SetActive(true);


            ObjectPool.Instance.ReturnObject(gameObject);
            spriteRenderer.color = Color.white;
            yield break;
        }
    }

}

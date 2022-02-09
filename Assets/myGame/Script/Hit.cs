using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hit : MonoBehaviour
{
    public int playerDamage = 10;

    private Vector2 reactVec;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int currentHp;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameObject canvas;

    private void init()
    {
        currentHp = enemyData.Hp;
        hpBar.fillAmount = 1.0f;
        canvas.SetActive(false);
    }

    private void OnEnable()    // 오브젝트풀에서 다시 활성화 될 때 초기화
    {
        Debug.Log("지옥에서 돌아왔다");
        init();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        init(); // 시작하면 현재피는 풀피
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("weapon")) // 적들이 플레이어의 무기와 충돌하면
        {
            StopCoroutine(hideHp());        
            StartCoroutine(hideHp());

            canvas.SetActive(true);      // 맞으면 체력바가 보이게 함
            currentHp -= playerDamage;
            hpBar.fillAmount = (float)currentHp / enemyData.Hp;

            reactVec = new Vector2(transform.position.x - collision.transform.position.x,
            transform.position.y - collision.transform.position.y).normalized;
            
            StartCoroutine(onDamage(reactVec));
        }
    }

    IEnumerator hideHp()                      // 체력바가 보이고 10초동안 공격 안 받으면 체력바 숨김
    {
        yield return new WaitForSeconds(10);
        canvas.SetActive(false);
    }

    IEnumerator onDamage(Vector2 reactVec)                     // 피격, 넉백 효과 
    {
        if(currentHp <= 0)  // 죽으면 그냥 코루틴 빠져나옴
        {
            ObjectPool.Instance.ReturnObject(gameObject);
            sr.color = Color.white;
            yield break;
        }

        sr.color = Color.red;
        rb.AddForce(reactVec * 10f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);

        rb.AddForce(-reactVec * 10f, ForceMode2D.Impulse);
        sr.color = Color.white;
    }
}

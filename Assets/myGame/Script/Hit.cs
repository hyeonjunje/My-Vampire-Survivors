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

    private void OnEnable()    // ������ƮǮ���� �ٽ� Ȱ��ȭ �� �� �ʱ�ȭ
    {
        Debug.Log("�������� ���ƿԴ�");
        init();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        init(); // �����ϸ� �����Ǵ� Ǯ��
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("weapon")) // ������ �÷��̾��� ����� �浹�ϸ�
        {
            StopCoroutine(hideHp());        
            StartCoroutine(hideHp());

            canvas.SetActive(true);      // ������ ü�¹ٰ� ���̰� ��
            currentHp -= playerDamage;
            hpBar.fillAmount = (float)currentHp / enemyData.Hp;

            reactVec = new Vector2(transform.position.x - collision.transform.position.x,
            transform.position.y - collision.transform.position.y).normalized;
            
            StartCoroutine(onDamage(reactVec));
        }
    }

    IEnumerator hideHp()                      // ü�¹ٰ� ���̰� 10�ʵ��� ���� �� ������ ü�¹� ����
    {
        yield return new WaitForSeconds(10);
        canvas.SetActive(false);
    }

    IEnumerator onDamage(Vector2 reactVec)                     // �ǰ�, �˹� ȿ�� 
    {
        if(currentHp <= 0)  // ������ �׳� �ڷ�ƾ ��������
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

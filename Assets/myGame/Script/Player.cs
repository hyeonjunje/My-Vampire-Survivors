using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public gameManager gm;
    public GameObject GameOverPanel;

    public float playerDamage = 5.0f;
    public float Speed = 10.0f;
    public float maxHp = 100;
    public float curHp;
    public Image hpImage;

    private Animator anim;
    [SerializeField] private GameObject Character;

    private bool isMove;
    private Vector2 movement;

    public Doodle doodleAbility;
    public bool hasAbilityDoodle;
    float DoodleTime;

    public Whip whipAbility;
    public bool hasAbilityWhip;
    float WhipTIme;

    void Start()
    {
        curHp = maxHp;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        move();
        attack();
    }
    void move()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isMove = movement.magnitude != 0;
        Vector2 dir = movement.normalized;

        if (isMove)
        {
            if (movement.x > 0)
            {
                Character.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if(movement.x < 0)
            {
                Character.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            transform.Translate(dir * Time.deltaTime * Speed);
            anim.SetBool("isRun", true);
        }
        else { anim.SetBool("isRun", false); }
    }

    void attack()
    {
        if (hasAbilityDoodle)
        {
            DoodleTime += Time.deltaTime;
            if(DoodleTime >= doodleAbility.coolTime)
            {
                DoodleTime = 0f;

                GameObject doodle = ObjectPool.Instance.GetObject("Doodle");
                Doodle doodleA = doodle.GetComponent<Doodle>();
                doodleA.player = transform;

                doodle.SetActive(true);
                doodleA.logic();
            }
        }
        if(hasAbilityWhip)
        {
            WhipTIme += Time.deltaTime;
            if(WhipTIme >= whipAbility.coolTime)
            {
                WhipTIme = 0;

                GameObject whip = ObjectPool.Instance.GetObject("Whip");
                Whip whipA = whip.GetComponent<Whip>();
                whipA.player = transform;

                whip.SetActive(true);
                whipA.logic();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10f);
    }

    public void levelUp(float soulAmount)
    {
        gm.levelUp(soulAmount);
    }

    public void printPlayerHp(float damage)
    {
        curHp -= damage;
        hpImage.fillAmount = curHp / maxHp;
        if(curHp <= 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }
}

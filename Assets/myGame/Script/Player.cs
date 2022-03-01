using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public gameManager gm;

    public int playerDamage = 10;
    public float Speed = 5.0f;
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
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        move();
        attack();
    }
    void move()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isMove = movement.magnitude != 0;
        
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
            transform.Translate(movement * Time.deltaTime * Speed);
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
}

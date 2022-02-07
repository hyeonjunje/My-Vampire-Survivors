using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5.0f;
    private Animator animator;

    [SerializeField] private GameObject Character;
    


    private bool isMove;
    private Vector2 movement;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
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
                Character.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if(movement.x < 0)
            {
                Character.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            transform.Translate(movement * Time.deltaTime * Speed);
        }
    }

    void attack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isAttack");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string enemyName;
    public string EnemyName { get { return enemyName; } }

    [SerializeField] private int hp;
    public int Hp { get { return hp; } }

    [SerializeField] private int damage;
    public float Damage { get { return damage; } }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
}

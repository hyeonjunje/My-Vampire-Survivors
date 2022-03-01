using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public int abilityLV;
    public float damageIncreaseRate;
    public float totalDamage;
    public float coolTime;
    public string abilityName;
    public string abilityContent;
    public Sprite abilitySprite;

    public Transform player; // 플레이어의 위치

}

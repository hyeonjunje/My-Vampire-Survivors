using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public Player player;
    public gameManager gm;
    public GameObject rewardPanel;

    public GameObject doodlePrefab;
    public GameObject whipPrefab;

    public string[] abilityName;
    public string[] abilityContent;
    public Sprite[] sprites;

    public Text[] abilityNameText;
    public Text[] abilityContentText;
    public Image[] abilitySpriteImage;
    public Text[] abilityLevelText;

    public Image[] takenAbilityImage;
    public Image[] takenPassiveAbilityImage;

    List<int> abilityOrder;

    // 채찍은 기본 무기로
    int[] level = { 0, 1, 0, 0 };
    int takenAbility = 0;
    int takenPassiveAbility = 0;

    private void Awake()
    {
        // 채찍은 기본으로 탑재
        takenAbilityImage[takenAbility].sprite = sprites[1];
        takenAbility++;
    }

    public void display()
    {
        abilityOrder = new List<int>();
        selectRandomAbility();

        for (int i = 0; i < abilitySpriteImage.Length; i++)
        {
            int randomAbility = abilityOrder[i];
            abilityNameText[i].text = abilityName[randomAbility];
            abilityContentText[i].text = abilityContent[randomAbility];
            abilitySpriteImage[i].sprite = sprites[randomAbility];
            abilityLevelText[i].text = "레벨: " + level[randomAbility].ToString();
        }
    }
    
    void displayAbility(int abilityNumber)
    {
        if(abilityNumber < 2)   // ability
        {
            takenAbilityImage[takenAbility].sprite = sprites[abilityNumber];
            takenAbility++;
        }
        else                   // passive
        {
            takenPassiveAbilityImage[takenPassiveAbility].sprite = sprites[abilityNumber];
            takenPassiveAbility++;
        }
    }

    void selectRandomAbility()
    {
        int currentAbility = Random.Range(0, sprites.Length);
        for(int i = 0; i < sprites.Length;)
        {
            if(abilityOrder.Contains(currentAbility)) currentAbility = Random.Range(0, sprites.Length);
            else { abilityOrder.Add(currentAbility); i++; }
        }
    }

    public void rankUp1()
    {
        string ability = abilityNameText[0].text;
        switchAbility(ability);
    }
    public void rankUp2()
    {
        string ability = abilityNameText[1].text;
        switchAbility(ability);
    }
    public void rankUp3()
    {
        string ability = abilityNameText[2].text;
        switchAbility(ability);
    }

    void switchAbility(string ability)
    {
        Doodle doodleLogic = doodlePrefab.GetComponent<Doodle>();
        Whip whipLogic = whipPrefab.GetComponent<Whip>();
        switch (ability)
        {
            case "Doodle":
                if (!player.hasAbilityDoodle) { player.hasAbilityDoodle = true; displayAbility(0); }
                else
                {
                    for (int i = 0; i < ObjectPool.Instance.doodlePool.Count; i++)
                    {
                        Doodle doodle = ObjectPool.Instance.doodlePool[i].GetComponent<Doodle>();
                        doodle.damage += 2;
                        ObjectPool.Instance.doodlePool[i].transform.localScale += Vector3.one * 3f;
                    }
                }
                level[0]++;
                break;
            case "Whip":
                if (!player.hasAbilityWhip) { player.hasAbilityWhip = true; displayAbility(1); }
                else
                {
                    for (int i = 0; i < ObjectPool.Instance.whipPool.Count; i++)
                    {
                        Whip whip = ObjectPool.Instance.whipPool[i].GetComponent<Whip>();
                        whip.damage += 2;
                        ObjectPool.Instance.whipPool[i].transform.localScale += Vector3.one * 3f;
                    }
                }
                level[1]++;
                break;
            case "Power Up":
                if (level[2] == 0) displayAbility(2);
                player.playerDamage *= 1.1f;
                level[2]++;
                break;
            case "Speed Up":
                if (level[3] == 0) displayAbility(3);
                player.Speed *= 1.05f;
                level[3]++;
                break;
        }
        gm.resume();
    }
}

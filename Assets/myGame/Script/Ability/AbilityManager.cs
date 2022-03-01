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

    List<int> abilityOrder;

    // 채찍은 기본 무기로
    int[] level = { 0, 1, 0, 0 };

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
        Doodle doodleLogic = doodlePrefab.GetComponent<Doodle>();
        Whip whipLogic = whipPrefab.GetComponent<Whip>();
        switch (ability)
        {
            case "Doodle":
                if (!player.hasAbilityDoodle) player.hasAbilityDoodle = true;
                else
                {
                    doodlePrefab.transform.localScale += Vector3.one * 3;
                    doodleLogic.damage *= 1.1f;
                }
                level[0]++;
                break;
            case "Whip":
                if (!player.hasAbilityWhip) player.hasAbilityWhip = true;
                else
                {
                    whipPrefab.transform.localScale += Vector3.one * 3;
                    whipLogic.damage *= 1.1f;
                }
                level[1]++;
                break;
            case "PowerUp":
                doodleLogic.damageIncreaseRate += 0.1f;
                whipLogic.damageIncreaseRate += 0.1f;
                level[2]++;
                break;
            case "Speed Up":
                player.Speed *= 1.05f;
                level[3]++;
                break;
        }
        gm.resume();
    }
    public void rankUp2()
    {
        string ability = abilityNameText[1].text;
        Doodle doodleLogic = doodlePrefab.GetComponent<Doodle>();
        Whip whipLogic = whipPrefab.GetComponent<Whip>();
        switch (ability)
        {
            case "Doodle":
                if (!player.hasAbilityDoodle) player.hasAbilityDoodle = true;
                else
                {
                    doodlePrefab.transform.localScale += Vector3.one * 3;
                    doodleLogic.damage *= 1.1f;
                }
                level[0]++;
                break;
            case "Whip":
                if (!player.hasAbilityWhip) player.hasAbilityWhip = true;
                else
                {
                    whipPrefab.transform.localScale += Vector3.one * 3;
                    whipLogic.damage *= 1.1f;
                }
                level[1]++;
                break;
            case "PowerUp":
                doodleLogic.damageIncreaseRate += 0.1f;
                level[2]++;
                break;
            case "Speed Up":
                player.Speed *= 1.05f;
                level[3]++;
                break;
        }
        gm.resume();
    }
    public void rankUp3()
    {
        string ability = abilityNameText[2].text;
        Doodle doodleLogic = doodlePrefab.GetComponent<Doodle>();
        Whip whipLogic = whipPrefab.GetComponent<Whip>();
        switch (ability)
        {
            case "Doodle":
                if (!player.hasAbilityDoodle) player.hasAbilityDoodle = true;
                else
                {
                    doodlePrefab.transform.localScale += Vector3.one * 3;
                    doodleLogic.damage *= 1.1f;
                }
                level[0]++;
                break;
            case "Whip":
                if (!player.hasAbilityWhip) player.hasAbilityWhip = true;
                else
                {
                    whipPrefab.transform.localScale += Vector3.one * 3;
                    whipLogic.damage *= 1.1f;
                }
                level[1]++;
                break;
            case "PowerUp":
                doodleLogic.damageIncreaseRate += 0.1f;
                level[2]++;
                break;
            case "Speed Up":
                player.Speed *= 1.05f;
                level[3]++;
                break;
        }
        gm.resume();
    }
}

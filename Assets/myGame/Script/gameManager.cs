using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public int level;
    public float experience;
    public float maxExperience;
    public Text lvText;
    public Image soulBar;
    public GameObject rewardPanel;
    public AbilityManager abilityManager;


    public GameObject player;
    //public ObjectPool objectPool;
    public Text playTimeText;

    private int wave = 1;
    private int gameTime = 0;
    private float gameTimer = 0f;

    bool isSpawn;

    string[] enemyNames = { "Slime", "Sans" };

    private void Update()
    {
        playTime();
        spawn();
    }
    void playTime()
    {
        gameTimer += Time.deltaTime;
        if (gameTimer >= 1)
        {
            gameTime++;
            playTimeText.text = string.Format("{0:D2}:{1:D2}", gameTime / 60, gameTime % 60);
            gameTimer = 0;
        }
    }

    // �� ��ȯ�ϴ°� (player�� ��ġ���� �������� ���� ��ȯ)
    // 5�ʸ��� 10������ ��ȯ
    void spawn()
    {
        if(gameTime > 5 * wave)  
        {
            wave++;
            for (int i = 0; i < 10; i++)
            {
                int ran = Random.Range(0, 10);
                int index;
                if (ran > 3) index = 0;
                else index = 1;

                var obj = ObjectPool.Instance.GetObject(enemyNames[index]);
                EnemyController enemyController = obj.GetComponent<EnemyController>();
                enemyController.player = player;
                obj.transform.position = ranPos();
                obj.SetActive(true);
            }
            
        }
    }

    Vector3 ranPos()
    {
        float ranAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 pos = player.transform.position + new Vector3(70 * Mathf.Cos(ranAngle), 70 * Mathf.Sin(ranAngle), 0);
        return pos;
    }
    public void levelUp(float expAmount)
    {
        experience += expAmount;
        
        lvText.text = "LV : " + level.ToString();
        soulBar.fillAmount = experience / maxExperience;

        if (experience >= maxExperience)
        {
            rewardPanel.SetActive(true);

            level++;
            experience = experience - maxExperience;
            maxExperience *= 1.1f;

            Time.timeScale = 0;
            abilityManager.display();
        }
    }
    public void resume()
    {
        Time.timeScale = 1;
        rewardPanel.SetActive(false);
    }
}

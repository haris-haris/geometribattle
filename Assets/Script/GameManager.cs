using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerObject;

    public GameObject cameraObject;
    GameObject[] gameObjects;

    CameraScript cameraScript;
    PlayerScript playerScript;

    public GameObject enemyFollower;
    public GameObject enemyBoss;
    public GameObject enemyChase;

    MainMenuScript MainMenuScript;
    EnemyChasingScript enemyChaseScript;
    EnemyFollowerScript enemyScript;
    EnemyBossScript enemyBossScript;


    public GameObject[] spawnPositions;

    int enemyCount = 10;
    int randomEnemy;

    //ui stuff
    public TextMeshProUGUI scoreTxtTMP;
    public TextMeshProUGUI waveTxtTMP;
    public TextMeshProUGUI timeTxtTMP;
    public Image[] HpBar;
    public Image[] WeaponsIcon;

    public int score;
    public float spawnWait;
    public float startWait;
    public float startWaitBoss;
    public float waveWait;
    float timeleft = 180.0f;
    float bossCountDown = 20;

    public int curWave = 1;

    bool bossStage = false;
    bool stoppedcourenemy = false;
    bool stoppedcourboss = false;

    public bool gameover = false;
    public bool win = false;
    // Use this for initialization
    void Start()
    {
        //Instantiate(playerObject);
        
        if (instance == null)
            instance = this;

        //calling other scripts
        cameraScript = cameraObject.GetComponent<CameraScript>();
        enemyScript = enemyFollower.GetComponent<EnemyFollowerScript>();
        playerScript = playerObject.GetComponent<PlayerScript>();
        enemyBossScript = enemyBoss.GetComponent<EnemyBossScript>();
        enemyChaseScript = enemyChase.GetComponent<EnemyChasingScript>();

        cameraScript.player = playerObject;

        //setting ui text
        scoreTxtTMP.enabled = true;
        waveTxtTMP.enabled = true;
        timeTxtTMP.enabled = true;

        //setting timescale
        Time.timeScale = 1;

        //BOSS STAGE CONDITION
        //curWave = 5;
        //timeleft = 0;

        //start game
        if (timeleft <= 180.0f || curWave <= 0)
            StartCoroutine("SpawnEnemy");
        
        //enemyFollower.transform.position = spawnPositions[1].transform.position;
        //Instantiate(enemyFollower);

        //enemyFollower.transform.position = spawnPositions[3].transform.position;
        //Instantiate(enemyFollower);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossStage)
            timeleft -= Time.deltaTime;
        if (bossStage)
        {
            timeleft = 10;
            bossCountDown -= Time.deltaTime;
        }
       //all waves cleared
        if (timeleft < 0 && curWave > 4)
        {
            timeTxtTMP.enabled = false;
            waveTxtTMP.text = "Wave : Boss Wave";
            timeleft = 10;
            enemyCount = 3;
            stoppedcourenemy = true;
            StartCoroutine("SpawnBoss");
        }

        if (bossCountDown < 0)
        {
            bossCountDown = 0;
            enemyCount = 3;
        }

        //game over win/lose
        if (playerScript.life <= 0)
        {
            gameOver();
        }
        if (win == true)
        {
            stoppedcourboss = true;
            youWin();
        }

        //ui hp bar
        if (playerScript.life == 4)
        {
            HpBar[4].enabled = false;
            HpBar[3].enabled = true;
            HpBar[2].enabled = true;
            HpBar[1].enabled = true;
            HpBar[0].enabled = true;
        }
         if (playerScript.life == 3)
        {
            HpBar[4].enabled = false;
            HpBar[3].enabled = false;
            HpBar[2].enabled = true;
            HpBar[1].enabled = true;
            HpBar[0].enabled = true;
        }
         if (playerScript.life == 2)
        {
            HpBar[4].enabled = false;
            HpBar[3].enabled = false;
            HpBar[2].enabled = false;
            HpBar[1].enabled = true;
            HpBar[0].enabled = true;
        }
         if (playerScript.life == 1)
        {
            HpBar[4].enabled = false;
            HpBar[3].enabled = false;
            HpBar[2].enabled = false;
            HpBar[1].enabled = false;
            HpBar[0].enabled = true;
        }
         if (playerScript.life == 0)
        {
            HpBar[4].enabled = false;
            HpBar[3].enabled = false;
            HpBar[2].enabled = false;
            HpBar[1].enabled = false;
            HpBar[0].enabled = false;
        }
        //ui text ingame
        scoreTxtTMP.text = "Score: " + score.ToString();
        if (curWave <= 4)
        {
            timeTxtTMP.text = "Time Remaining: " + timeleft.ToString("0");
            waveTxtTMP.text = "Wave :" + curWave.ToString() + "/5";
        }

        //UI selected Weapon
        if (playerScript.isAutoOn == true)
            WeaponsIcon[0].color = new Color(1.0f, 1.0f, 1.0f);
        else
            WeaponsIcon[0].color = new Color(0.25f, 0.25f,0.25f);

        if (playerScript.isBurstOn == true)
            WeaponsIcon[1].color = new Color(1.0f, 1.0f, 1.0f);
        else
            WeaponsIcon[1].color = new Color(0.25f, 0.25f, 0.25f);

        if (playerScript.isTwoWayOn == true)
            WeaponsIcon[3].color = new Color(1.0f, 1.0f, 1.0f);
        else
            WeaponsIcon[3].color = new Color(0.25f, 0.25f, 0.25f);

        if (playerScript.isChaseOn == true)
            WeaponsIcon[2].color = new Color(1.0f, 1.0f, 1.0f);
        else
            WeaponsIcon[2].color = new Color(0.25f, 0.25f, 0.25f);
    }
    
    IEnumerator SpawnEnemy()
    {
        stoppedcourenemy = false;
        if (stoppedcourenemy)
        {
            yield break;
        }
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                int randomSpawnIndex = Random.Range(0, 8);

                enemyFollower.transform.position = spawnPositions[randomSpawnIndex].transform.position;
                enemyChase.transform.position = spawnPositions[randomSpawnIndex].transform.position;

                randomEnemy = Random.Range(0, 2);
                if (randomEnemy == 0)
                    Instantiate(enemyFollower);
                else
                    Instantiate(enemyChase);
                //spawn wait = 1
                yield return new WaitForSeconds(spawnWait);
            }
            //wave wait = 20
            yield return new WaitForSeconds(waveWait);
            curWave++;
        }
    }

    IEnumerator SpawnBoss()
    {
        bossStage = true;
        stoppedcourboss = false;
        //enemyCount = 5;
        if (stoppedcourboss)
        {
            yield break;
        }
        //start wait boss = 1
        yield return new WaitForSeconds(startWaitBoss);
        int randomSpawnIndex = Random.Range(0, 8);
            enemyBoss.transform.position = spawnPositions[randomSpawnIndex].transform.position;
        for (int i = 0; i < enemyCount; i++)
        {
            enemyFollower.transform.position = spawnPositions[randomSpawnIndex].transform.position;
            enemyChase.transform.position = spawnPositions[randomSpawnIndex].transform.position;
            randomEnemy = Random.Range(0, 2);
            if (randomEnemy == 0)
                Instantiate(enemyFollower);
            else
                Instantiate(enemyChase);
            //enemyFollower.transform.position = spawnPositions[randomSpawnIndex].transform.position;
            //Instantiate(enemyFollower);
            //spawn wait = 1
            yield return new WaitForSeconds(spawnWait);
        }
        Instantiate(enemyBoss);

    }
    void gameOver()
    {
        gameover = true;
        Application.LoadLevel(2);
    }
    void youWin()
    {
        Application.LoadLevel(3);
    }
}

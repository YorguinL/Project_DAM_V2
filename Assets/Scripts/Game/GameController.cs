using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public GameObject playerPrefab;
    public GameObject portalPrefab;
    public GameObject saveGame;
    public GameObject gameOver;
    private static int health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 2f;
    private static float fireRate = 1f;
    private static int countAuxHT = 0;
    public static int countKilledEnemies = 0;
    public static int level = 1;
    public static int score = 0;
    public static int savedGame = 0;
    public static int Health { get => health; set => health = value;}
    public static int MaxHealth { get => maxHealth; set => maxHealth = value;}
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value;}
    public static float FireRate { get => fireRate; set => fireRate = value;}
    public Text healtItems;
    public Text fireRateItems;
    public Text speedItems;
    public Text kills;
    public Text timerText;
    public Text levelText;
    public Text scoreText;
    private int minutes = 2;
    private int seconds = 30;
    private int m, s;
 
    
    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    private void Awake() {
        
        if(instance == null) {
            instance = this;
        } 

    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = level+"";
        healtItems.text = " x " + PlayerController.countHealth;
        fireRateItems.text = " x " + PlayerController.countFireRate;
        speedItems.text = " x " + PlayerController.countPlayerSpeed;
        kills.text = " x " + countKilledEnemies;

        isDead();
        IncreaseLevel();
    }

    // Subtract damage from player
    public static void DamagePlayer(int damage) {

        health -= damage;

    }

    public static void IncreaseHealth(int healthItems){
        if(health < maxHealth){
            if(healthItems > countAuxHT)
                health += 1;
        }
        countAuxHT = healthItems;
    }

    public static void IncreaseFireRate(int fireRateItems){

        switch(fireRateItems){

            case(3):
                fireRate = 0.85f;
            break;

            case(6):
                fireRate = 0.70f;
            break;

            case(18):
                fireRate = 0.55f;
            break;

            case(32):
                fireRate = 0.30f;
            break;
        }
    }

    public static void IncreasePlayerSpeed(int speedItems){

        switch(speedItems){

            case(3):
                moveSpeed = 2.5f;
            break;

            case(6):
                moveSpeed = 2.75f;
            break;

            case(18):
                moveSpeed = 3f;
            break;

            case(32):
                moveSpeed = 3.5f;
            break;
        }
    }

    public void IncreaseLevel(){        

        switch(countKilledEnemies){

            case(5):
                if(level != 2){
                    level = 2;
                    StopTimer();                    
                }
            break;

            case(10):
                if (level != 3){
                    level = 3;
                    StopTimer();
                }
                break;

            case(15):
                if (level != 4){
                    level = 4;
                    StopTimer();
                }
                break;

            case(29):
                if (level != 5){
                    level = 5;
                    StopTimer();
                }
                break;

            case(30):
                if (level != 6){
                    level = 6;
                    StopTimer();
                }
                break;

            case(41):
                GameOver();
            break;
        }
    }

    void StartTimer(){
        m = minutes;
        s = seconds;
        WriteTimer(m, s);
        Invoke("UpdateTimer", 1f );
    }

    void StopTimer(){
        m = minutes;
        s = seconds;
        WriteTimer(m, s);
        Invoke("UpdateTimer", 0f);
        Time.timeScale = 0;
        saveGame.SetActive(true);
    }

    void UpdateTimer()
    {
        if(s != 0){
            s--;
        } else {
            if(m == 0 && s == 0){
                GameOver();
                
            } else {
                m--;
                s = 59;
            }
        }
        WriteTimer(m, s);
        Invoke("UpdateTimer", 1.2f);
    }

    void WriteTimer(int m, int s){
        if(s < 10) {
            timerText.text = m.ToString() + ":0" + s.ToString();
        } else {
            timerText.text = m.ToString() + ":" + s.ToString();
        }
    }

    public void Continue(){
        saveGame.SetActive(false);
        Time.timeScale = 1f;
        StartTimer();
    }


    public void SaveAndExit(){
        savedGame = 1;
        Time.timeScale = 1f;
        Invoke("UpdateTimer", 1f);

        DataBaseController ddbb = new DataBaseController();
        ddbb.Start();
        ddbb.UpdateRecord(InputsController.NickName, score);

        SceneManager.LoadScene(0);
    }

    public static void calScore(){

        score = ((PlayerController.countHealth * 2) + (PlayerController.countFireRate * 2) + (PlayerController.countPlayerSpeed * 2) + (countKilledEnemies + 5))*level;
        
    }

    public void GameOver(){
        calScore();
        scoreText.text = score + "";
        Time.timeScale = 0f;
        Invoke("UpdateTimer", 0f);
        
        DataBaseController ddbb = new DataBaseController();
        ddbb.Start();
        ddbb.UpdateRecord(InputsController.NickName, score);

        gameOver.SetActive(true);
    }

    public void isDead(){
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void SaveAndRanking(){
        savedGame = 1;
        Time.timeScale = 1f;

        DataBaseController ddbb = new DataBaseController();
        ddbb.Start();
        ddbb.UpdateRecord(InputsController.NickName, score);

        SceneManager.LoadScene(1);
    }
}

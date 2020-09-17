using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject portalPrefab;
    public GameObject [] enemyPrefabs = new GameObject [2];
    public float spawnRate;
    float nextSpawn = 0.0f;
    int count = 0;
    public GameObject [] itemsPrefabs = new GameObject [3];
    float spawnRateItems;
    float nextSpawnItems = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemy");
        itemsPrefabs = Resources.LoadAll<GameObject>("Prefabs/Collection");
    }

    // Update is called once per frame
    void Update()
    {
        nextLevel();
        levels(GameController.level);
    }

    void levels(int lvl){

        switch(lvl){

            case(1):
                spawnRate = 7f;
                spawnRateItems = 40f;
                PortalController.time = 30f;
                if(count < 6){
                    CreateEnemies();
                    CreateItems();
                }
            break;

            case(2):
                spawnRate = 3f;
                spawnRateItems = 45f;
                PortalController.time = 20f;
                if(count < 12){
                    CreateEnemies();
                }
            break;

            case(3):
                spawnRate = 3.5f;
                spawnRateItems = 35f;
                PortalController.time = 15f;
                if(count < 15){
                    CreateEnemies();

                }
            break;

            case(4):
                spawnRate = 2f;
                spawnRateItems = 30f;
                PortalController.time = 10f;
                if(count < 20){
                    CreateEnemies();
                }
            break;

            case(5):
                spawnRate = 2f;
                spawnRateItems = 25f;
                PortalController.time = 10f;
                if(count < 20){
                    CreateEnemies();
                }
            break;

            case(6):
                spawnRate = 1f;
                spawnRateItems = 15f;
                PortalController.time = 5f;
                if(count < 20){
                    CreateEnemies();
                }
            break;
        }
    }

    void nextLevel(){
        int currentLevel = 1;
        if(GameController.level > currentLevel){
            currentLevel = GameController.level;
            count = 0;
        }
    }

    void CreateEnemies(){
    
        if(Time.time > nextSpawn){
            nextSpawn = Time.time + spawnRate;
            int rnd = Random.Range(0,2);
            GameObject enemy = (GameObject)Instantiate(enemyPrefabs[rnd]);
            enemy.transform.position = portalPrefab.transform.position;
            count++;
        }
    }

    public void CreateItems(){

        if(Time.time > nextSpawnItems){
            nextSpawnItems = Time.time + spawnRateItems;
            GameObject fireRateItem = (GameObject)Instantiate(itemsPrefabs[0]);
            PositionsController.NewPosition(fireRateItem);

            GameObject healthItem = (GameObject)Instantiate(itemsPrefabs[1]);
            PositionsController.NewPosition(healthItem);

            GameObject speedItem = (GameObject)Instantiate(itemsPrefabs[2]);
            PositionsController.NewPosition(speedItem);
        }
    }
}

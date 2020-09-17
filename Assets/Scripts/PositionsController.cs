using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void NewPosition(GameObject gameObject){

        int rnd = Random.Range(0,2);
        
        if(rnd == 0){
            NewPositionLeft(gameObject);
        } else {
            NewPositionRight(gameObject);
        }
    }

    public static void NewPositionLeft(GameObject gameObject){
        int posX = Random.Range(-4,-17);
        int posY = Random.Range(-8,8);

        gameObject.transform.position = new Vector3(posX, posY, 0);
    }
    public static void NewPositionRight(GameObject gameObject){
        int posX = Random.Range(4,17);
        int posY = Random.Range(-8,8);

        gameObject.transform.position = new Vector3(posX, posY, 0);
    }
}

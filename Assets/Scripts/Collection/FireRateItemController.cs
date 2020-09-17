using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateItemController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.tag == "Player"){
            PlayerController.countFireRate++;
            Destroy(gameObject);    
            GameController.IncreaseFireRate(PlayerController.countFireRate);      
        }
    }
}

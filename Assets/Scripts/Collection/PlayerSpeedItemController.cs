﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedItemController : MonoBehaviour
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
            PlayerController.countPlayerSpeed++;
            Destroy(gameObject);           
            GameController.IncreasePlayerSpeed(PlayerController.countPlayerSpeed); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveCheck : MonoBehaviour
{
    public bool playerIsAlive;
    public PlayerHealth Health;
    // Start is called before the first frame update
    void Start(){
        playerIsAlive = true;
    }

    // Update is called once per frame
    void Update(){
        aliveCheck();
    }
    public void aliveCheck(){
        if (Health.health > 1){
            playerIsAlive = true;
        }
        else if (Health.health < 1){
            playerIsAlive = false;
        }
        
    }
    public void gameOver(){
        if(playerIsAlive == false){
            Time.timeScale = 0;
            
        }
    }
}
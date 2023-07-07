using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnMenu : MonoBehaviour
{
    public GameObject respawnScreen;
    public bool timerActive = false;
    public Text timerText; 
    int timeLeft;
    public float decTimeLeft = 5f;
    public Rigidbody2D playerRB;
    public Transform player;
    public PlayerHealth playerHealth;
    private Vector3 spawnPoint;
    public LevelManager levelManager;

    void Start(){
        spawnPoint = player.position;
    }
    void Update()
    {
        if (timerActive){
            handleRespawnTimer();
        }
    }

    public void setRespawnMenuActive(){
        respawnScreen.SetActive(true);
        timerActive = true;
        timerText.gameObject.SetActive(true);
        Time.timeScale = 1f;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        levelManager.respawnPlayer();
    }

    public void setRespawnMenuInactive(){
        if (timeLeft <= 0){
            timerActive = false;
            decTimeLeft = 5f;
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            respawnScreen.SetActive(false);
            playerHealth.health = 1;
        }
    }

    void handleRespawnTimer(){
        timeLeft = Mathf.RoundToInt(decTimeLeft);
        if(timeLeft > 0){
            decTimeLeft -= Time.deltaTime;
            timerText.text = "Respawn in " + timeLeft.ToString();
        }
        else if (timeLeft < 1){
            timerText.gameObject.SetActive(false);
            timerActive = false;
        }

    }
}


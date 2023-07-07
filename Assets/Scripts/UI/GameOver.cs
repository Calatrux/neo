using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AliveCheck Alive;
    public GameTimer Timer;
    public PlayerMovement Move;
    public GameObject gameOverScreenObject;
    public GameObject respawnMenu;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start(){
        gameOverScreenObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update(){
        gameOverScreen();
    }

    public void gameOverScreen(){
        if(Timer.timeRunning == false){
            Time.timeScale = 0;
            gameOverScreenObject.SetActive(true);
            respawnMenu.SetActive(false);
            audioManager.PlayLoseClip();
        }
    }
    public void gameOver(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
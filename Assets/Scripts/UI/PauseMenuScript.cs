using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerMovement movement;
    public bool pauseOn = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (pauseOn == false){
                openPauseMenu();
            }
            else if (pauseOn == true){
                closePauseMenu();
            }
        }
    }
    public void openPauseMenu(){
        pauseMenu.SetActive(true);
        pauseOn = true;
        Time.timeScale = 0;
        movement.grounded = true;

    }
    public void closePauseMenu(){
        pauseMenu.SetActive(false);
        pauseOn = false;
        Time.timeScale = 1;
    }
    
}

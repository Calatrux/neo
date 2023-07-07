using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public Text timerText;
    public AudioSource blip;
    // Start is called before the first frame update
    void Awake(){
        mainMenuScreen.SetActive(true);
        Time.timeScale = 0;
        timerText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        
    }
    
    public void playButton(){
        mainMenuScreen.SetActive(false);
        Time.timeScale = 1;
        timerText.gameObject.SetActive(true);
    }

    public void playBlip(){
        blip.Play();
    }
}
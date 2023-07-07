using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialController : MonoBehaviour
{
    public GameObject[] tutorialScreens;
    public int currentScreen = 0;
    public GameObject tutorial;
    public bool tutorialActive = false;
    public GameObject prompt;
    public GameObject mainMenu;

    public void goForward(){
        currentScreen++;
    }

    public void goBack(){
        currentScreen--;
    }

    void Update(){
        if (tutorialActive){
            if (currentScreen < 0){
                currentScreen = 0;
            }
            if (currentScreen > tutorialScreens.Length - 1){
                currentScreen = tutorialScreens.Length - 1;
            }
            for (int i = 0; i < tutorialScreens.Length; i++){
                if (i == currentScreen){
                    tutorialScreens[i].SetActive(true);
                }
                else{
                    tutorialScreens[i].SetActive(false);
                }
            }
        }
    }

    public void activateTutorial(){
        tutorial.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void setTutorialOn(){
        prompt.SetActive(false);
        tutorialActive = true;
    }

    public void deactivateTutorial(){
        tutorial.SetActive(false);
        tutorialActive = false;
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GameTimer : MonoBehaviour
{
    public float decTimeLeft;
    public int timeLeft;
    public bool timeRunning;
    public Text timerText;
    public bool developerMode; // set to true to disable timer

    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    public float maxVignette;
    public float maxChromAbberation;
    public Volume volume;
    public float maxTime;



    private void Start() {
        timeRunning = true;
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out chromaticAberration);
    }
    private void Update() {
        if(timeRunning == true && developerMode == false){
            gameTimer();
            changePostProcessing();
        }
    }

    public void gameTimer(){
        timeLeft = Mathf.RoundToInt(decTimeLeft);
        if(timeLeft > 0){
        decTimeLeft -= Time.deltaTime;
        timerText.text = "Time Left: " + timeLeft.ToString();
        }
        else if (timeLeft < 1){
            timerText.text = "Time Has Ran Out!";
            timeRunning = false;
        }

    }

    void changePostProcessing(){
        vignette.intensity.value = (maxTime - decTimeLeft) / maxTime * maxVignette; // more time elapsed = higher vignette intensity
        chromaticAberration.intensity.value = 0.3f + (maxTime - decTimeLeft) / maxTime * maxChromAbberation; // same for chromatic abberation
    }

}
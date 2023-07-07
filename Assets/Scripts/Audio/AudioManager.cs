using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource runSource;
    public AudioSource musicSource;

    public AudioClip jumpClip;
    public AudioClip runClip;
    public AudioClip leverClip;
    public AudioClip platformClip;
    public AudioClip portalClip;
    public AudioClip deathClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public int currentMusicIndex = 0;

    public void Update(){
        RotateMusic();
    }
    

    public void PlayJumpClip()
    {
        audioSource.volume = 0.28f;
        audioSource.clip = jumpClip;
        audioSource.Play();
    }

    public void PlayRunClip()
    {
        runSource.clip = runClip;
        runSource.Play();
    }  

    public void PlayLeverClip()
    {
        audioSource.volume = 0.5f;
        audioSource.clip = leverClip;
        audioSource.Play();
    }

    public void PlayPlatformClip()
    {
        audioSource.clip = platformClip;
        audioSource.Play();
    }


    public void PlayPortalClip()
    {
        audioSource.volume = 1f;
        audioSource.clip = portalClip;
        audioSource.Play();
    }

    public void PlayDeathClip()
    {
        audioSource.volume = 1f;
        audioSource.clip = deathClip;
        audioSource.Play();
    }

    public void PlayWinClip()
    {
        print("win!");
        audioSource.volume = 1f;
        audioSource.clip = winClip;
        audioSource.Play();
    }

    public void PlayLoseClip()
    {
        audioSource.volume = 1f;
        audioSource.clip = loseClip;
        audioSource.Play();
    }

    public void RotateMusic(){
        AudioClip[] music = {music1, music2, music3};
        if (!musicSource.isPlaying){
            currentMusicIndex += 1;
            if (currentMusicIndex > music.Length - 1){
                currentMusicIndex = 0;
            }
            musicSource.clip = music[currentMusicIndex];
            musicSource.Play();
        }
    }


}

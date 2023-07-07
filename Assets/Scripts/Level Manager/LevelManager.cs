using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelValues[] levels;
    public Transform player;
    public Rigidbody2D playerRB;
    public int currentLevel;
    int[] levelNumbers;
    public int currentLevelIndex;
    public GameTimer gameTimer;
    public GameObject winScreen;
    public Animator crossfade;
    public AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start(){ // creates a list with all the level numbers and randomly shuffles them to determine the order of levels
        levelNumbers = new int[levels.Length];
        for (int i = 0; i < levels.Length; i++) {
            levelNumbers[i] = levels[i].level;
        }
        for (int i = 0; i < levelNumbers.Length; i++) {
            int temp = levelNumbers[i];
            int randomIndex = Random.Range(i, levelNumbers.Length);
            levelNumbers[i] = levelNumbers[randomIndex];
            levelNumbers[randomIndex] = temp;
        }
        currentLevel = levelNumbers[0];
        currentLevelIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (levels[currentLevel - 1].gameObject.activeSelf == false) { // if level is finished, advances to the next level
            levels[currentLevel - 1].gameObject.SetActive(true);
            gameTimer.decTimeLeft = levels[currentLevel - 1].time;
            gameTimer.maxTime = levels[currentLevel - 1].time;
            player.position = levels[currentLevel - 1].spawnPoint.position;
        }

        if (currentLevelIndex == levelNumbers.Length - 1) { // if all levels are finished, win screen is activated as the win condition has been fufilled 
            if (!winScreen.activeSelf) {
                audioManager.PlayWinClip();
            }
            winScreen.SetActive(true);
            crossfade.gameObject.SetActive(false);
            gameTimer.timerText.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    
    public void AdvanceLevel() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel(){ // increments current level while using a crossfade animation to smoothly transition from scenes
        crossfade.gameObject.SetActive(true);
        crossfade.SetBool("start", true);
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(1f);
        levels[currentLevel - 1].gameObject.SetActive(false);
        currentLevelIndex++;
        currentLevel = levelNumbers[currentLevelIndex];
        audioManager.audioSource.volume = 0;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(1f);
        crossfade.SetBool("start", false);
        crossfade.gameObject.SetActive(false);

    }

    public void respawnPlayer(){ // resets player to spawn point of the new level
        player.position = levels[currentLevel - 1].spawnPoint.position;
    }

    public void QuitGame(){ // doesn't quite work with webgl
        Application.Quit();
    }
}

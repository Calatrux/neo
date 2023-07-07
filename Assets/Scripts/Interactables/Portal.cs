using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public LevelManager levelManager;
    public AudioManager audioManager;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            audioManager.PlayPortalClip();
            levelManager.AdvanceLevel(); // advances level when player enters the player
        }
    }
}

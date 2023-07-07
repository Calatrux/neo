using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health = 1;
    public RespawnMenu respawnMenu;
    public AudioManager audioManager;
    // Start is called before the first frame update
    public void takeDamage(int damage){
        health -= damage;
        if (health <= 0){
            audioManager.PlayDeathClip();
            respawnMenu.setRespawnMenuActive();
        }
    }
}

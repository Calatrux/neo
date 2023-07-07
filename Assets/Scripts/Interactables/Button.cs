using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite pressedButton;
    public Sprite unpressedButton;
    public bool playerOnButton = false;
    public bool boxOnButton = false;
    public Laser laserLine;
    public GameObject laserLineObject;
    public bool inversed;
    public AudioManager audioManager;
    // Start is called before the first frame update

    void Update(){
        if (playerOnButton || boxOnButton){ // turns laser on when pressed (could be inversed)
            spriteRenderer.sprite = pressedButton;
            laserLine.laserActive = inversed ? true : false;
            if (laserLine.laserActive == true){
                laserLine.gameObject.SetActive(true);
            }

        }
        if (!playerOnButton && !boxOnButton){ // turns laser off when not being pressed (also could be inversed)
            spriteRenderer.sprite = unpressedButton;
            laserLine.laserActive = inversed ? false : true;
            if (laserLine.laserActive == true){
                laserLine.gameObject.SetActive(true);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other) // determines what's pressing button 
    {
        if (other.CompareTag("Player")){
            playerOnButton = true;
        }else if (other.CompareTag("Box")){
            boxOnButton = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            playerOnButton = false;
        }else if (other.CompareTag("Box")){
            boxOnButton = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool playerInRange = false;
    public int leverState = 0;
    public SpriteRenderer leverSprite;
    public Sprite[] leverSprites;
    public Transform objectToMove;
    public Vector3[] positions;
    int currentPosition;
    public AudioManager audioManager;

    void Update(){
        if (playerInRange && Input.GetKeyDown(KeyCode.E)){ // if player is in range and they press e, change lever state
            leverState++;
            if (leverState > 4){
                leverState = 0;
            }
            audioManager.PlayLeverClip();
        }

        leverSprite.sprite = leverSprites[leverState];

        MoveObject();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            playerInRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            playerInRange = false;
        }
    }

    void MoveObject(){ // moves an object based on laser state and a predetermined array of positions for the object to move to
        currentPosition = leverState;
        objectToMove.position = positions[currentPosition];
    }

}

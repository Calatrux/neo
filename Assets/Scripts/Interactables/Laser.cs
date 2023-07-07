using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public bool laserActive;
    public enum Orientation {Left, Right, Up, Down};
    public Orientation orientation;
    public Transform laserShooter;
    public ParticleSystem laserParticles;
    public PlayerHealth playerHealth;
    public int maxReflections;
    Vector3 origDir;
    public SpriteRenderer spriteRenderer;
    public Sprite shootingLaser;
    public Sprite normalLaser;
    Vector3 finalDir = Vector3.zero;

    void Start(){ // really messy but it works so idc
        Physics2D.queriesStartInColliders = false;
        laserParticles.gameObject.SetActive(true);
        if (orientation == Orientation.Left){ // changes rotation based on orientation
            transform.eulerAngles = new Vector3(0, 0, 180);
            laserShooter.eulerAngles = new Vector3(0, 0, 180);
            origDir = -transform.up;
        }else if (orientation == Orientation.Right){
            transform.eulerAngles = new Vector3(0, 0, 0);
            laserShooter.eulerAngles = new Vector3(0, 0, 0);
            origDir = transform.up;
        }else if (orientation == Orientation.Up){
            transform.eulerAngles = new Vector3(0, 0, 90);
            laserShooter.eulerAngles = new Vector3(0, 0, 90);
            origDir = -transform.right;
        }else if (orientation == Orientation.Down){
            transform.eulerAngles = new Vector3(0, 0, -90);
            laserShooter.eulerAngles = new Vector3(0, 0, -90);
            origDir = transform.right;
        }
    }

    void Update(){
        drawLaser();
        changeLaserSprite();
    }


    public void drawLaser(){
        laserParticles.gameObject.SetActive(true); // resets laser position and points
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, laserShooter.position);

        RaycastHit2D hit = Physics2D.Raycast(laserShooter.position, origDir, 50); // uses raycasts to determine when and where its hitting something

        bool isMirror = false;
        Vector2 mirrorHitPoint = new Vector2(0,0);

        for (int i = 0; i < maxReflections; i++){ // raycasts multiple times in case of multiple mirrors, capped at maxReflections
            lineRenderer.positionCount++; // adds a point

            if (hit.collider != null){
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point); // adds hit coords to line

                if (hit.collider.CompareTag("Player")){ // kills player
                    playerHealth.takeDamage(1);
                }

                if (hit.collider.CompareTag("Bulb")){ // fills laser
                    hit.collider.GetComponent<Bulb>().bulbActive = true;
                }


                isMirror = false;
                if (hit.collider.CompareTag("Mirror")){ // if mirror hit, laser reflects off it, and a new raycast is fired and the process repeats.
                    mirrorHitPoint = (Vector2)hit.point;
                    Vector3 dir = getReflectionDirection(hit.collider.GetComponent<Mirror>());
                    finalDir = convertToEuler(getReflectionDirection(hit.collider.GetComponent<Mirror>()));
                    hit = Physics2D.Raycast(hit.point, dir);
                    isMirror = true;
                }else{break;}
            }else{ // nothing to hit anymore
                if (isMirror){ 
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, mirrorHitPoint + (Vector2)transform.up * 50);
                    break;
                }else{
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, laserShooter.position + transform.right * 50);
                    break;
                }
            }
        }

        laserParticles.gameObject.SetActive(true); // moves and rotates particles based on last hit position in line renderer.
        laserParticles.transform.eulerAngles = finalDir;
        laserParticles.transform.position = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        laserParticles.Play();
        
    }

    Vector3 getReflectionDirection(Mirror mirror){ // gets new dir after mirror is hit
        mirror.mirrorHit = true;
        if (mirror.orientation == (Mirror.Orientation)Orientation.Left){
            return -transform.up;
        }else if (mirror.orientation == (Mirror.Orientation)Orientation.Right){
            return transform.up;
        }else if (mirror.orientation == (Mirror.Orientation)Orientation.Up){
            return -transform.right;
        }else{
            return transform.right;
        } 
    }

    void changeLaserSprite(){ // changes laser sprite whether its active or not
        if (laserActive){
            spriteRenderer.sprite = shootingLaser;
            gameObject.SetActive(true);
        }else{
            spriteRenderer.sprite = normalLaser;
            gameObject.SetActive(false);
        }
    }

    Vector3 convertToEuler(Vector3 orig){ // transform dir -> vector3
        if (orig == transform.up){
            return new Vector3(0,0,0);
        }else if (orig == -transform.up){
            return new Vector3(0,0,180);
        }else if (orig == transform.right){
            return new Vector3(0,0,-90);
        }else{
            return new Vector3(0,0,90);
        }
    }

}

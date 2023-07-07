using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public int currentSprite;
    public float animationSpeed;
    public bool bulbActive;
    public bool bulbFull;
    public bool bulbEmpty;
    public Animator[] doorAnimator;
    public EdgeCollider2D[] doorCollider;
    bool canChange = true;
    public bool inversed;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (bulbActive && currentSprite < 6 && canChange){
            StartCoroutine(Animate(1));
        }
        if (!bulbActive && currentSprite > 0 && canChange){
            StartCoroutine(Animate(-1));
        }

        bulbFull = (currentSprite == 6);
        bulbEmpty = (currentSprite == 0);

        if (currentSprite >= 0 && currentSprite <= 6){ // displays current sprite
            spriteRenderer.sprite = sprites[currentSprite];
        }
        bulbActive = false;
        ChangeDoor();
    }

    IEnumerator Animate(int increment){ // increases or decreases current bulb frame
        canChange = false;
        yield return new WaitForSeconds(animationSpeed);
        currentSprite += increment;
        canChange = true;
    }

    void ChangeDoor(){ // if bulb full, change door, inverse if bulb becomes empty
        if (bulbFull && (doorAnimator[0].GetBool("active") == inversed ? true : false) && !doorAnimator[0].GetBool("animating")){
            StartCoroutine(DoorCooldown());
            foreach (Animator animator in doorAnimator){
                animator.SetBool("active", inversed ? false : true);
            }
            foreach (EdgeCollider2D edge in doorCollider){ // loops through all door and enables their collider
                edge.enabled = inversed ? true : false;
            }
            audioManager.PlayPlatformClip();
        }

        if (bulbEmpty && (doorAnimator[0].GetBool("active") == inversed ? false : true) && !doorAnimator[0].GetBool("animating")){
            StartCoroutine(DoorCooldown());
            foreach (Animator animator in doorAnimator){
                animator.SetBool("active", inversed ? true : false);
            }
            foreach (EdgeCollider2D edge in doorCollider){
                edge.enabled = inversed ? false : true;
            }
            audioManager.PlayPlatformClip();
        }

    }
    
    IEnumerator DoorCooldown(){ // animating true if it needs to be animated, false when animation is finished in 0.35s
        foreach (Animator animator in doorAnimator){
            animator.SetBool("animating", true);
        }
        yield return new WaitForSeconds(0.35f);
        foreach (Animator animator in doorAnimator){
            if (animator.gameObject.activeSelf){
                animator.SetBool("animating", false);
            }
        }
    }
}

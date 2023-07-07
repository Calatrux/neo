using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public enum Orientation {Left, Right, Up, Down};
    public Orientation orientation;
    public bool mirrorHit;
    public Vector3 reflectionDir;
    public SpriteRenderer spriteRenderer;
    public Sprite normalMirror;
    public Sprite hitMirror;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update(){
        if (orientation == Orientation.Left){ // rotates mirror based off orientation
            transform.eulerAngles = new Vector3(0, 0, 180);
            reflectionDir = -transform.up;
        }else if (orientation == Orientation.Right){
            transform.eulerAngles = new Vector3(0, 0, 0);
            reflectionDir = transform.up;
        }else if (orientation == Orientation.Up){
            transform.eulerAngles = new Vector3(0, 0, 90);
            reflectionDir = -transform.right;
        }else if (orientation == Orientation.Down){
            transform.eulerAngles = new Vector3(0, 0, -90);
            reflectionDir = transform.right;
        }

        if (mirrorHit){ // changes sprite based on whether the mirror is hit or not
            spriteRenderer.sprite = hitMirror;
        }else{
            spriteRenderer.sprite = normalMirror;
        }
        mirrorHit = false;
    }

    public Vector3 HitMirror(){ // returns reflection dir to laser when it gets hit
        mirrorHit = true;
        if (orientation == Orientation.Left){
            return reflectionDir;
        }
        if (orientation == Orientation.Up){
            return reflectionDir;
        }
        return reflectionDir;
    }
}

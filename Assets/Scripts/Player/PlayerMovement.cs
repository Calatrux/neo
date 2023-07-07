using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    Vector2 moveInput;
    public float moveSpeed;
    public float acceleration;
    public float deceleration;
    public float velPower;
    public float frictionConstant;
    public float moveInAirMultiplier;
    bool facingRight = true;
    [Header("Vertical Movement")]
    public bool grounded;
    public LayerMask groundLayer;
    public float jumpForce;
    public float groundedTime;
    public float jumpTime;
    public float lateJumpBooster;
    float lastGroundedTime;
    float lastJumpTime;
    public float jumpCut;
    public float fallInAirMultiplier;
    bool canJump = true;
    bool wasOnGround;



    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public ParticleSystem dust;
    public TrailRenderer trail;
    public AudioManager audioManager;

    void Update(){
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        wasOnGround = grounded;

        // Two raycasts on either leg of player, better than middle raycast if player only has one leg on an edge
        grounded = (Physics2D.Raycast(new Vector3(transform.position.x + 0.12f, transform.position.y, transform.position.z), Vector2.down, 0.6f, groundLayer) ||
        (Physics2D.Raycast(new Vector3(transform.position.x - 0.12f, transform.position.y, transform.position.z), Vector2.down, 0.6f, groundLayer)));

        if (!wasOnGround && grounded){
            StartCoroutine(JumpSqueeze(1.35f, 0.75f, 0.05f));
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.X))){
            grounded = false;
        }

        if (Input.GetKey(KeyCode.E)){
            StartCoroutine(WaitForInteractAnimation());
        }

        CalculateBetterJump();
        Animate();
    }

    void CreateDust(){
        dust.Play();
    }

    void Animate(){
        animator.SetFloat("horizontal", Mathf.Abs(moveInput.x));
        animator.SetFloat("vertical", rb.velocity.y);
    }

    void ApplyFriction(){
        if (grounded && Mathf.Abs(moveInput.x) < 0.01f){
            float frictionAmount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionConstant));
            frictionAmount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -frictionAmount, ForceMode2D.Impulse);
        }
    }

    void Flip(){
        if (moveInput.x > 0 && !facingRight || moveInput.x < 0 && facingRight){
            facingRight = !facingRight;
            transform.localRotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
            if (grounded) {
                CreateDust();
            }
        }
    }
    void FixedUpdate(){
        MovePlayer();
        ApplyFriction();
        Flip();
    }

    void MovePlayer(){
        float targetSpeed = moveInput.x * moveSpeed;
        if (!grounded && Mathf.Abs(moveInput.x) > 0.5){
            targetSpeed *= moveInAirMultiplier;
        }
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

        if (Mathf.Abs(rb.velocity.x) > 0.05 && grounded){
            if (audioManager.runSource.isPlaying == false){
                audioManager.PlayRunClip();
            }
        }else{
            if (audioManager.runSource.isPlaying == true){
                audioManager.runSource.Stop();
            }
        }
    }

    void CalculateBetterJump(){
        if (grounded){
            lastGroundedTime = groundedTime;
            animator.SetBool("grounded", true);
        }else{
            lastGroundedTime -= Time.deltaTime;
            animator.SetBool("grounded", false);
        }

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.UpArrow)){
            lastJumpTime = jumpTime;
        }else{
            lastJumpTime -= Time.fixedDeltaTime;
        }

        if (lastJumpTime > 0 && lastGroundedTime > 0 && canJump){
            Jump();
            audioManager.PlayJumpClip();
            StartCoroutine(JumpCooldown());
        }

        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.UpArrow)) && rb.velocity.y > 0){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCut);
        }

        if (rb.velocity.y < 0){
            rb.gravityScale = fallInAirMultiplier;
        }else{
            rb.gravityScale = 1f;
        }

        if (!wasOnGround && grounded){
            CreateDust();
        }
    }

    void Jump(){
        if (lastJumpTime < jumpTime){
            jumpForce *= lateJumpBooster;
        }else{
            jumpForce = 6.3f;
        }
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        lastJumpTime = jumpTime;
        StartCoroutine(JumpSqueeze(0.75f, 1.35f, 0.05f));
    }

    IEnumerator JumpCooldown(){
        canJump = false;
        yield return new WaitForSeconds(0.25f);
        canJump = true;
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) {
        Vector3 originalSize = transform.localScale;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

        transform.localScale = Vector3.one;

    }

    IEnumerator WaitForInteractAnimation(){
        animator.SetBool("interact", true);
        yield return new WaitForSeconds(0.35f);
        animator.SetBool("interact", false);
    }

}



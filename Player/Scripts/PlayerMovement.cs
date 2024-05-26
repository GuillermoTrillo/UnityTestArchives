
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    //raycast variables
    public Vector2 boxSize;
    public float castDistance = 1;
    public LayerMask groundLayer;

    //the action was called with the button click
    Vector2 moveAction;
    float jumpAction;
    float dashAction;

    //movement Variables
    float speed = 300f;
    float jumpHeight = 100f;
    
    //dash Variables
    float dashVelocity = 15f;
    float dashTime = 0.5f;
    float dashingCooldown = 1f;
    bool allowToDash = true;
    bool isDashing = false;
    
    //this gameObject variables
    GameObject thisGameObject;
    private Rigidbody2D rb2D;
    TrailRenderer tr;


    //the Start() function will simply define the components in their respective variables
    private void Start() {
        thisGameObject = gameObject;
        tr = thisGameObject.GetComponent<TrailRenderer>();
        rb2D = thisGameObject.GetComponent<Rigidbody2D>();
    }

    //* The OnSomething functions receive the InputAction call, and they transform it into 
    //* variables which can be used later for button recognition
    public void OnMove(InputAction.CallbackContext context) 
    {
        moveAction = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context) {
        jumpAction = context.ReadValue<float>();
    }
    public void OnDash(InputAction.CallbackContext context) {
        dashAction = context.ReadValue<float>();
    } 
    
    //* each function does a movement part. Dash is a CoRoutine, so it can have a determined time
    private void Move() {
        rb2D.velocity = new Vector2(moveAction.x * speed * Time.deltaTime, rb2D.velocity.y);
    }  
    private void Jump() {
        rb2D.AddForce(new Vector2(rb2D.velocity.x, jumpHeight), ForceMode2D.Impulse);
    }    
    private IEnumerator Dash() {
        float movementDirection = moveAction.x;
        allowToDash = false;
        isDashing = true;
        rb2D.velocity = new Vector2(movementDirection * dashVelocity, rb2D.velocity.y);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        allowToDash = true;
    }

    //* casts a raycast under the player in the form of a box, to check if he's grounded or not
    public bool IsGrounded() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) {
            return true;
        }
        else {
            return false;
        }
    }
    //* makes the raycast visible.
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up* castDistance, boxSize);
    }

    //* Fixed Update does all the calls for the movement functions, as they work with physics.
    private void FixedUpdate() {
        if(jumpAction > 0 && IsGrounded()) {
            Jump();
        }

        // if player is dashing, they can not move nor try to dash again.
        if(isDashing == true) {
            return;
        }
        if(dashAction > 0 && allowToDash == true) {
            StartCoroutine(Dash());
        }

        Move();
    }
}


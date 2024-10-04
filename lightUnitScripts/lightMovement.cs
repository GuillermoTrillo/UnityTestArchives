using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class lightMovement : MonoBehaviour
{
    //*raycast variables
    public Vector2 boxSizeLight;
    public float castDistanceLight = 1;
    public LayerMask groundLayerLight;

    //*the action was called with the button click
    Vector2 moveAction;
    float jumpAction;
    float dashAction;

    //*movement Variables
    float maximumSpeed = 750;
    float speed = 0f;
    float acceleration = 0f;
    float accelerationTime = 0.6f;

    //*jump Variables
    float jumpHeight = 15f;
    bool isJumping = false;
    float coyoteTime = 0.15f;
    float coyoteTimeCounter; 

    //*dash Variables
    float dashAmount = 3;
    
    float dashVelocity = 90f;
    float dashTime = 0.10f;
    float dashingCooldown = 0.3f;
    bool allowToDash = true;
    bool isDashing = false;
    //JumpDash Variables
    
    
    //*this gameObject variables
    GameObject thisGameObject;
    private Rigidbody2D rb2D;
    TrailRenderer tr;

    void Start()
    {
        thisGameObject = gameObject;
        tr = thisGameObject.GetComponent<TrailRenderer>();
        rb2D = thisGameObject.GetComponent<Rigidbody2D>();
    }

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

    private void Move() {
        if(allowToDash == false) {
            speed = maximumSpeed;
        }
        else if (moveAction.x == 1 || moveAction.x == -1)
        {
            acceleration = maximumSpeed / accelerationTime;
            speed += acceleration * Time.deltaTime;
        }
        else {
            speed = 0;
        }
        
        if(speed > maximumSpeed)
        {
            speed = maximumSpeed;
        }
        Vector2 velocity = new Vector2(speed * moveAction.x * Time.deltaTime, rb2D.velocity.y);
        rb2D.velocity = velocity;    
        
        }  
    
    private void Jump() {
        coyoteTimeCounter = 0;
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHeight);
        // rb2D.AddForce(new Vector2(rb2D.velocity.x, jumpHeight), ForceMode2D.Impulse);
    }    
    //Suposed to do a especific jump when jumping at the same time that you dash
    private IEnumerator DashJump() {

        //StopCoroutine(Dash());
        allowToDash = false;
        isDashing = true;
        isJumping = true;
        jumpHeight = 10;
        dashVelocity = 40f;
        rb2D.velocity = new Vector2(moveAction.x * rb2D.transform.localScale.x * dashVelocity, jumpHeight);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        yield return new WaitUntil(() => IsGrounded() == true);
        tr.emitting = false;
        isDashing = false;
        isJumping = false;
        dashVelocity = 100f;
        jumpHeight = 15f;
    }

    private IEnumerator Dash() {
        Debug.Log("Dashing!");
        allowToDash = false;
        isDashing = true;
        rb2D.velocity = new Vector2(moveAction.x * rb2D.transform.localScale.x * dashVelocity, 0);
        tr.emitting = true;
      
        yield return new WaitForSeconds(dashTime);
        Debug.Log("dash Ended");
        isDashing = false;
        tr.emitting = false;
        coyoteTimeCounter = 0;

          if( !IsGrounded()) {
            yield return new WaitUntil(() => IsGrounded() == true);
        }
        else {
            yield return new WaitForSeconds(dashingCooldown);
        }
        
        Debug.Log("cooldown off");
        allowToDash = true;
    }

    //* casts a raycast under the player in the form of a box, to check if he's grounded or not
    public bool IsGrounded() {
        if (Physics2D.BoxCast(transform.position, boxSizeLight, 0, -transform.up, castDistanceLight, groundLayerLight)) {
            return true;
        }
        else {
            return false;
        }
    }
    //* makes the raycast visible.
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistanceLight, boxSizeLight);
    }

    private void Update() {
        if(IsGrounded()) {
            coyoteTimeCounter = coyoteTime;
            accelerationTime = 0.3f;
        } else {
            accelerationTime = 0.6f;
            coyoteTimeCounter -= Time.deltaTime;
        }
    }
    //* Fixed Update does all the calls for the movement functions, as they work with physics.
    private void FixedUpdate() {
        
        
        // if player is dashing, they can not move nor try to dash again.
        if(isDashing == true && jumpAction > 0 && IsGrounded()) {
            StartCoroutine(DashJump());
            return;
        } 
        else if (isDashing == true){
            return;
        }

        if(jumpAction > 0 && coyoteTimeCounter > 0) {
            Jump();
        }
        if(!isDashing && !isJumping) {
            Move();
        }
        
       
        if(dashAction > 0 && allowToDash == true && moveAction.x != 0) {
            StartCoroutine(Dash());
        }
    }

}



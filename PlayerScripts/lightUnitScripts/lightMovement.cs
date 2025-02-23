using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class lightMovement : PlayerMovement
{ 

    //*movement Variables
    float maximumSpeed = 750;
    float speed = 0f;
    float acceleration = 0f;
    float accelerationTime = 0.6f;

    //*jump Variables
    float jumpHeight = 15f;
    float coyoteTime = 0.15f;
    float coyoteTimeCounter; 

    //*dash Variables
    float amountOfDashes = 3;
    
    float dashVelocity = 90f;
    float dashTime = 0.10f;
    float dashingCooldown = 0.3f;
    bool allowToDash = true;
    bool isDashing = false;
    //JumpDash Variables

    //* Move function moves the character with a certain amount of acceleration
    private void Move() {
        //this way, when dashing, there will be no acceleration when
        //coming out of the dash
        if(allowToDash == false) {
            speed = maximumSpeed;
        }
        else if (moveAction.x == 1 || moveAction.x == -1)
        {
            //creates acceleration
            acceleration = maximumSpeed / accelerationTime;
            speed += acceleration * Time.deltaTime;
        }
        else {
            //restarts the speed of the player
            speed = 0;
        }
        
        if(speed > maximumSpeed)
        {
            speed = maximumSpeed;
        }
        Vector2 velocity = new Vector2(speed * moveAction.x * Time.deltaTime, rb2D.velocity.y);
        rb2D.velocity = velocity;    
        
    }  
    
    //* Makes the jump possible
    private void Jump() {
        coyoteTimeCounter = 0;
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHeight);
    }    
    
    //*Supposed to do a especific jump when jumping at the same time that you dash
    private IEnumerator DashJump() {
        //defines the variables needed to do the dashjump and stop any other action
        allowToDash = false;
        isDashing = true;       
        jumpHeight = 12;
        dashVelocity = 40f;
        rb2D.velocity = new Vector2(moveAction.x * rb2D.transform.localScale.x * dashVelocity, jumpHeight);
        tr.emitting = true;

        //waits for the end of the dash
        yield return new WaitForSeconds(dashTime);
        yield return new WaitUntil(() => IsGrounded() == true);
        
        //restarts the variables
        tr.emitting = false;
        isDashing = false;
        dashVelocity = 100f;
        jumpHeight = 15f;
    }
    
    //* The coroutine that makes the dash posible
    private IEnumerator Dash() {
        //defines the variables needed to do the dash and stop any other action but jumping
        allowToDash = false;
        isDashing = true;
        //increases the velocity
        rb2D.velocity = new Vector2(moveAction.x * rb2D.transform.localScale.x * dashVelocity, 0);
        tr.emitting = true;
        //restarts the variables
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        tr.emitting = false;
        coyoteTimeCounter = 0;    
        amountOfDashes--;

        if( !IsGrounded()) {
            yield return new WaitUntil(() => IsGrounded() == true);
        }
        else {
            yield return new WaitForSeconds(dashingCooldown);
        }
        allowToDash = true;
    }
    
    //* casts a raycast under the player in the form of a box, to check if he's grounded or not
    
    //* makes the raycast visible in-Scene mode

    private void stopMovementByMagnet() {
        if(Player.getisInMagnet() == true) {
            rb2D.velocity = Vector2.zero;
            rb2D.gravityScale = 0;
        }
        else
            rb2D.gravityScale = 3;
    }
    //* The Update variable is only used to define CoyoteTime
    private void Update() {
        if(IsGrounded()) {
            coyoteTimeCounter = coyoteTime;
            accelerationTime = 0.3f;
        } else {
            accelerationTime = 0.6f;
            coyoteTimeCounter -= Time.deltaTime;
        }
    }
    //* Fixed Update does all the calls for the movement functions
    private void FixedUpdate() {
        stopMovementByMagnet();
        //will start the dashjump, or stop any other action, if already in dash
        if(isDashing == true) {
            if(jumpAction > 0 && coyoteTimeCounter > 0) {
                StopCoroutine(Dash());
                StartCoroutine(DashJump());
            }
            return;
        }
        //It lets the dash start if called, and allows 
        //the dash amount recharge
        if(allowToDash == true) {
            if(dashAction > 0 && moveAction.x != 0 && amountOfDashes >= 1) {
                StartCoroutine(Dash());
            }
            if(amountOfDashes < 3 && IsGrounded() != true) {
                amountOfDashes = amountOfDashes + 0.05f;
            }
        }
        //activates the jump
        if(jumpAction > 0 && coyoteTimeCounter > 0) {
            Jump();
        }
        //activates basic movement
        if(!isDashing && Player.getIsInAir() == false) {
            Move();
        }
    }

}
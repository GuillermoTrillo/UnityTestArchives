using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeavyMovement : PlayerMovement
{

    


    //*the action was called with the button click
    float runningAction;

    //*movement Variables
    float maximumSpeed = 350;
    float speed = 0f; 
    float acceleration = 0f;
    float accelerationTime = 0.5f;
    float runningDirection;

    //*jump variables
    float jumpHeight = 17f;
    float coyoteTime = 0.15f;
    float coyoteTimeCounter; 


    public void OnRunning(InputAction.CallbackContext context) {
        runningDirection = moveAction.x;
        if(context.performed) {
            runningAction = 1f;
            tr.emitting = true;
            Running();
        } else if(context.canceled) {
            runningAction = 0f;
            tr.emitting = false;
            StopRunning();
        }
        
    }

    //* each function does a movement part. Dash is a CoRoutine, so it can have a determined time
    private void Move() {
        if (moveAction.x != 0)
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

        //responsible for aceleration. If player reaches maximum speed, speed will become a constant.
        //else, the speed will be exponentiatly get faster, until reached the limit.
    }  
    private IEnumerator Jump() {
        float direction = moveAction.x;
        float velo = rb2D.velocity.x;
    if(speed != maximumSpeed) {
        velo = direction * 6f;
    }
        yield return new WaitForSeconds(0.01f);
        //rb2D.AddForce(new Vector2(rb2D.velocity.x * 1.5f, jumpHeight), ForceMode2D.Impulse);
        rb2D.velocity = new Vector2(velo, jumpHeight);    
        
    }

    private void Running() {
        maximumSpeed = 750f;
        accelerationTime = 0.05f;
        
    }
    private void StopRunning() {
        maximumSpeed = 350f;
        accelerationTime = 0.5f;
        
    }
    private void ChangingDirectionsWhileRunning() {    
        accelerationTime = 1f;
        if(speed >= maximumSpeed) {
            runningDirection = moveAction.x;
            accelerationTime = 0.05f;
        }

    }
    //* casts a raycast under the player in the form of a box, to check if he's grounded or not

    //* makes the raycast visible.
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up* castDistance, boxSize);
    }

    private void Update() {  
        if ((moveAction.x != runningDirection) && runningAction == 1)  {
            ChangingDirectionsWhileRunning();
        }

        
        if(IsGrounded()) {
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }

    }
    //* Fixed Update does all the calls for the movement functions, as they work with physics.
    private void FixedUpdate() {
        
        if(jumpAction > 0 && moveAction.x != 0 && coyoteTimeCounter > 0) {
            StartCoroutine(Jump());
        }


        if(IsGrounded()) {
            Move();
        }
    }
}


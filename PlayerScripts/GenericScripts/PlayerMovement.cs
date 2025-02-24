using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{ 

    //*raycast variables
    protected Vector2 boxSize;
    protected float castDistance = 1f;
    [SerializeField] protected LayerMask groundLayer;


      //*the action was called with the button click
    protected float jumpAction;
    protected float dashAction;
    protected Vector2 moveAction;


    //*this gameObject variables
    protected GameObject thisGameObject;
    protected Rigidbody2D rb2D;
    protected TrailRenderer tr;
        
    private void Start() {
        thisGameObject = gameObject;
        tr = thisGameObject.GetComponent<TrailRenderer>();
        rb2D = thisGameObject.GetComponent<Rigidbody2D>();
        boxSize = new Vector2(0.54f, 0.44f);
    }
    
        //* OnX functions receive the input call and transfer it to a variable
    protected void OnMove(InputAction.CallbackContext context) {
        moveAction = context.ReadValue<Vector2>();
    }
    protected void OnJump(InputAction.CallbackContext context) {
        jumpAction = context.ReadValue<float>();
    }
    protected void OnDash(InputAction.CallbackContext context) {
        dashAction = context.ReadValue<float>();
    } 
    
    //* casts a raycast under the player in the form of a box, to check if he's grounded or not
    public bool IsGrounded() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) {
            Player.setIsInAir(false);
            return true;
        }
        else {
            return false;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
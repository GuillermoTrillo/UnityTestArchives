using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class LightMagnet : PlayerMagnet
{
  //related to the magnet actions

    RaycastHit2D playerTouchedMagnet;
    private float repelingAction;
    private float attractingAction;
    private int strengthOfMagnetAction = 3;

    public void OnAttractingMagnets(InputAction.CallbackContext context) {
        if (context.performed)
            attractingAction = 1;
        else
            attractingAction = 0;
    }
    public void OnRepelingMagnets(InputAction.CallbackContext context) {
        if (context.performed)
            repelingAction = 1;
        else
            repelingAction = 0;
        
    }
    
    private void FindIfMagnetIsOnTouch() {
        playerTouchedMagnet = Physics2D.BoxCast(transform.position, new Vector2(1.5f, 1.5f), 0, new Vector2(0,0), 0.01f, magnetLayer);
        
        if(playerTouchedMagnet)
            Player.setIsInMagnet(true);
        else
            Player.setIsInMagnet(false);
    }
    private void MoveIntoTheMagnet() {
        FindIfMagnetIsOnTouch();
        Player.setIsInAir(true);
        Vector2 directionOfThrow = (firstMagnetFound.transform.position - transform.position) * strengthOfMagnetAction;
        GetComponent<Rigidbody2D>().AddForce(directionOfThrow, ForceMode2D.Impulse);
    }
    private void MoveAwayFromTheMagnet() {
        FindIfMagnetIsOnTouch();
        Player.setIsInAir(true);
        Vector2 directionOfThrow = (firstMagnetFound.transform.position - transform.position) * strengthOfMagnetAction;
        GetComponent<Rigidbody2D>().AddForce(-directionOfThrow, ForceMode2D.Impulse);
    }

    void Update()
    {
        GetMagnetInMap();


        if(isMagnetOnRange) {
            if(repelingAction > 0 ) {
                MoveAwayFromTheMagnet();
            }
            if(attractingAction > 0) {
                MoveIntoTheMagnet();
            }
        }
        if(attractingAction == 0 && repelingAction == 0 ) {
            Player.setIsInMagnet(false);
        }
    }
}
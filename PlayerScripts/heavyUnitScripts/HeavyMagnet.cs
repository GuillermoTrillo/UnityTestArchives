using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class HeavyMagnet : PlayerMagnet
{
  //related to the magnet actions

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

    private void RepelTheTarget() {
        
    }
    private void AttractTheTarget() {

    }
    void Update()
    {
        GetMagnetInMap();

        if(isMagnetOnRange) {
            if(repelingAction > 0 ) {
                RepelTheTarget();
            }
            if(attractingAction > 0) {
                AttractTheTarget();
            }
        }
        if(attractingAction == 0 && repelingAction == 0 ) {
            Player.setIsInMagnet(false);
        }
    }
}
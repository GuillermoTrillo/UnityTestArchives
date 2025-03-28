using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class HeavyMagnet : PlayerMagnet
{
  //related to the magnet actions

    private float repelingAction;
    private float attractingAction;
    private int strengthOfMagnetAction = 1000;
    Rigidbody2D attachedObject;
    public void OnAttractingMagnets(InputAction.CallbackContext context) 
    {
        
        if (context.performed)
            attractingAction = 1;
        else
            attractingAction = 0;
    }
    public void OnRepelingMagnets(InputAction.CallbackContext context) 
    {
        if (context.performed)
            repelingAction = 1;
        else
            repelingAction = 0;
        
    }

    private void RepelTheTarget() 
    {
        Vector2 directionOfAttraction = (attachedObject.transform.position - transform.position) * strengthOfMagnetAction;
        //Debug.Log(directionOfAttraction);
        attachedObject.AddForce(directionOfAttraction, ForceMode2D.Force);
    }
    private void AttractTheTarget() 
    {
        Vector2 directionOfAttraction = (attachedObject.transform.position - transform.position) * strengthOfMagnetAction;
        attachedObject.AddForce(-directionOfAttraction, ForceMode2D.Force);
    }
    
    void Update()
    {
        GetMagnetInMap();
        
        if(isMagnetOnRange) {
            try {
                attachedObject = firstMagnetFound.rigidbody.GetComponent<Magnet>().getAttachedObject();    
            }
            catch {
                attachedObject = null;
            }
            
            if(repelingAction > 0 && attachedObject != null) {
                RepelTheTarget();
            }
            if(attractingAction > 0 && attachedObject != null) {
                AttractTheTarget();
            }
        }
    }
}
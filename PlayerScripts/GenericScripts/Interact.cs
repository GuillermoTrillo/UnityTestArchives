using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
public class Interact : MonoBehaviour
{

    public static event Action OnInteraction;
    float interactAction;
    float hasCollision;

    public void onInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
            interactAction = 1;
        else
            interactAction = 0;   
     }

    public void Interacting() {
        OnInteraction?.Invoke();
    }

    void Update()
    {
        if(interactAction > 0){
            Interacting();
        }
    }
}

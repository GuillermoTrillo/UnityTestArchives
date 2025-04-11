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

    public void onInteraction(InputAction.CallbackContext context) {
        interactAction = context.ReadValue<float>();
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

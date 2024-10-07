using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
public class Interact : MonoBehaviour
{

    public event Action interactAction;
    float lookAction;

    public void onInteraction(InputAction.CallbackContext context) {
        lookAction = context.ReadValue<float>();
    }

    public void Interacting() {
        interactAction?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        if(lookAction > 0){
        }
    }
}

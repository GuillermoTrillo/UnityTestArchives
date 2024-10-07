using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
public class Interact : MonoBehaviour
{

    public event Action Action;
    float lookAction;

    void onInteraction(InputAction.CallbackContext context) {
        lookAction = context.ReadValue<float>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

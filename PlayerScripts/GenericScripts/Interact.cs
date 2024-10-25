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
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        hasCollision = 1;
    }
    private void OnTriggerExit2D(Collider2D other) {
        hasCollision = 0;
    }

    void Update()
    {
        Debug.Log(interactAction + " and "+hasCollision);
        if(interactAction > 0 && hasCollision > 0){
            Interacting();
        }
    }
}

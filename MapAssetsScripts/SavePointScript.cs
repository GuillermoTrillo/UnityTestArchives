using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePointScript : MonoBehaviour
{
 private void OnTriggerEnter2D(Collider2D other) {
        Interact.OnInteraction += delegate ()
        {
            Cock();
        };
    }   

    private void Cock() {
        Debug.Log("bruh?");
    }
}

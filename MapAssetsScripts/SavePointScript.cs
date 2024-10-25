using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePointScript : MonoBehaviour
{
    public GameObject firstPlayer;
    public GameObject secondPlayer;
 private void OnTriggerEnter2D(Collider2D other) {
        Interact.OnInteraction += delegate ()
        {
            Cock();
        };
    }   

    private void Cock() {
        Destroy(firstPlayer);
    }
}

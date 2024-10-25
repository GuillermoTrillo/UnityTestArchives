using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePointScript : MonoBehaviour
{
 
    private void OnEnable() {
        Interact.OnInteraction += delegate ()
        {
        Debug.Log("bruh?");
        };   
    }

  
}

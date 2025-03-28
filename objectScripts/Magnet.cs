using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    Rigidbody2D attachedObject = null;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(attachedObject == null) {
            joinWithBody(other);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }   
    }

    private void joinWithBody(Collider2D other) {
        try
        {
            attachedObject = other.attachedRigidbody;
            attachedObject.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
        }
        catch (System.Exception)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            
        }
    }
    public Rigidbody2D getAttachedObject() {
        return attachedObject;
    }
}


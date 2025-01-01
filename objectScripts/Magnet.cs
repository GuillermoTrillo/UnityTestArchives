using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}

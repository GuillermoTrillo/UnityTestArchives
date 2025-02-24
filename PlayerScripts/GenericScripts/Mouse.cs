using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    // Related to the light that follows the Player
    [SerializeField] protected GameObject target;
    protected Quaternion rotation;

    //* simply gets the exact place of the cursor by locking it on the center and unlocking it
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }   
        
    private void LookAt() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;

        rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        target.transform.rotation = Quaternion.Slerp(target.transform.rotation, rotation, 10 * Time.deltaTime);
    }

    private void FixedUpdate() {
        LookAt();
    }
}

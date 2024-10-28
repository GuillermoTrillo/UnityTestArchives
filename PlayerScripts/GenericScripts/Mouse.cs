using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    [SerializeField] Transform target; //Assign to the object you want to rotate
    float magnetAction;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }
    public void OnShootingMagnets(InputAction.CallbackContext context) {
        magnetAction = context.ReadValue<float>();
    }

    private void LookAt() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        target.transform.rotation = Quaternion.Slerp(target.transform.rotation, rotation, 10 * Time.deltaTime);
    }

    private void ShootMagnet() {
        Debug.Log("asds");
    }
    private void FixedUpdate() {
        if(magnetAction > 0) {
            ShootMagnet();
        }
        LookAt();

    }
}

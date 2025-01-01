using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    public GameObject target; //Assign to the object you want to rotate
    Quaternion rotation;

    public GameObject magnetPrefab;
    int magnetQuantity = 5;
    List<GameObject> activeMagnetList = new List<GameObject>();
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }   

    public void OnShootingMagnets(InputAction.CallbackContext context) {
        if(magnetQuantity == 0) {
            return;
        }

        if (context.performed)
        {
            SpawnMagnet();
        }
    }
    public void CallingMagnetsBack(InputAction.CallbackContext context) {
        if(magnetQuantity == 5) {
            return;
        }

        if (context.performed)
        {
            DespawnMagnet();
        }
    }

    private void LookAt() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;

        rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        target.transform.rotation = Quaternion.Slerp(target.transform.rotation, rotation, 10 * Time.deltaTime);
    }

    private void SpawnMagnet() {
        magnetQuantity--;
        activeMagnetList.Add(Instantiate(magnetPrefab, target.transform.position, rotation));
        ShootMagnet();
    }
    private void ShootMagnet() {
        GameObject lastSpawned = activeMagnetList.LastOrDefault();
        Rigidbody2D magnetRigidBody = lastSpawned.GetComponent<Rigidbody2D>();
        Vector2 directionOfThrow = lastSpawned.transform.rotation * new Vector2(0, 50);
        magnetRigidBody.AddForce(directionOfThrow, ForceMode2D.Impulse);
    }
     private void DespawnMagnet() {       
        Destroy(activeMagnetList.LastOrDefault());
        activeMagnetList.Remove(activeMagnetList.LastOrDefault());
        magnetQuantity++;
    }

    private void FixedUpdate() {
        
        LookAt();

    }
}

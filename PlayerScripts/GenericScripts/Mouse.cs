using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    // Related to the light that follows the Player
    public GameObject target;
    Quaternion rotation;

    //related to the magnet shooting
    public GameObject magnetPrefab;
    int magnetQuantity = 5;
    List<GameObject> activeMagnetList = new List<GameObject>();
    public LayerMask magnetLayer;

    //related to the magnet actions
    private bool isMagnetOnRange = false;
    RaycastHit2D firstMagnetFound;
    RaycastHit2D playerTouchedMagnet;
    private float repelingAction;
    private float attractingAction;
    private int strengthOfMagnetAction = 10;

    //* simply gets the exact place of the cursor by locking it on the center and unlocking it
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }   

        //* Here are the shooting magnets' related functions
    //* Gets the button presses
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
    //* Spawns the magnet and saves it on the list
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
        Destroy(activeMagnetList.FirstOrDefault());
        activeMagnetList.Remove(activeMagnetList.FirstOrDefault());
        magnetQuantity++;
    }

    private void LookAt() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;

        rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        target.transform.rotation = Quaternion.Slerp(target.transform.rotation, rotation, 10 * Time.deltaTime);
    }

    public void OnAttractingMagnets(InputAction.CallbackContext context) {
        if (context.performed)
            attractingAction = 1;
        else
            attractingAction = 0;
    }
    public void OnRepelingMagnets(InputAction.CallbackContext context) {
        if (context.performed)
            repelingAction = 1;
        else
            repelingAction = 0;
        
    }
    private void GetMagnetInMap() {
          firstMagnetFound = Physics2D.BoxCast(target.transform.position, new Vector2(5,5), 110,target.transform.rotation * Vector2.up, 15, magnetLayer);
         
         if(firstMagnetFound) 
            isMagnetOnRange = true;  
         else 
            isMagnetOnRange = false;
    }
    private void FindIfMagnetIsOnTouch() {
        playerTouchedMagnet = Physics2D.BoxCast(transform.position, new Vector2(1,1), 0, new Vector2(0,0), 0.01f, magnetLayer);
        
        if(playerTouchedMagnet)
            Player.setIsInMagnet(true);
        else
            Player.setIsInMagnet(false);
    }
    private void MoveIntoTheMagnet() {
        FindIfMagnetIsOnTouch();
        Player.setIsInAir(true);
        Vector2 directionOfThrow = (firstMagnetFound.transform.position - transform.position) * strengthOfMagnetAction;
        GetComponent<Rigidbody2D>().AddForce(directionOfThrow, ForceMode2D.Impulse);
    }
    private void MoveAwayFromTheMagnet() {
        FindIfMagnetIsOnTouch();
        Player.setIsInAir(true);
        Vector2 directionOfThrow = (firstMagnetFound.transform.position - transform.position) * strengthOfMagnetAction;
        GetComponent<Rigidbody2D>().AddForce(-directionOfThrow, ForceMode2D.Impulse);
    }
    private void FixedUpdate() {
        LookAt();
        GetMagnetInMap();
        if(isMagnetOnRange) {
            if(repelingAction > 0 ) {
                MoveAwayFromTheMagnet();
            }
            if(attractingAction > 0) {
                MoveIntoTheMagnet();
            }
        }
        if(attractingAction == 0 && repelingAction == 0 ) {
            Player.setIsInMagnet(false);
        }

    }
}

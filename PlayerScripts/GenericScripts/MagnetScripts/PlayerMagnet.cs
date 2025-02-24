using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMagnet : Mouse
{
    //related to the magnet shooting
    [SerializeField] protected GameObject magnetPrefab;
    protected int magnetQuantity = 5;
    protected List<GameObject> activeMagnetList = new List<GameObject>();
    [SerializeField] protected LayerMask magnetLayer;
    protected RaycastHit2D firstMagnetFound;
    protected bool isMagnetOnRange = false;


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
    protected void SpawnMagnet() {
        magnetQuantity--;
        activeMagnetList.Add(Instantiate(magnetPrefab, target.transform.position, rotation));
        ShootMagnet();
    }
    protected void ShootMagnet() {
        GameObject lastSpawned = activeMagnetList.LastOrDefault();
        Rigidbody2D magnetRigidBody = lastSpawned.GetComponent<Rigidbody2D>();
        Vector2 directionOfThrow = lastSpawned.transform.rotation * new Vector2(0, 50);
        magnetRigidBody.AddForce(directionOfThrow, ForceMode2D.Impulse);
    }
    protected void DespawnMagnet() {       
        Destroy(activeMagnetList.FirstOrDefault());
        activeMagnetList.Remove(activeMagnetList.FirstOrDefault());
        magnetQuantity++;
    }
    protected void GetMagnetInMap() {
          firstMagnetFound = Physics2D.BoxCast(target.transform.position, new Vector2(5,5), 110,target.transform.rotation * Vector2.up, 15, magnetLayer);
         
         if(firstMagnetFound) 
            isMagnetOnRange = true;  
         else 
            isMagnetOnRange = false;
    }
}
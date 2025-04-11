using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class SavePointScript : MonoBehaviour
{
    public event Action interactingAction;
    private GameObject player;

    [SerializeField] private GameObject CHARACTERS;
    [SerializeField] private GameObject lightPrefab;
    [SerializeField] private GameObject heavyPrefab;
    void Start()
    {
        interactingAction = () => { 
            test();
        };
    }

    private void OnTriggerEnter2D(Collider2D other) {
        player = other.gameObject;
        Interact.OnInteraction += interactingAction;
    }   
    private void OnTriggerExit2D(Collider2D other) {
        Interact.OnInteraction -= interactingAction;
    }


    private Action test() {
        if(lightPrefab.CompareTag(player.tag)) {
            Destroy(player);
            Instantiate(heavyPrefab, player.transform.position, player.transform.rotation, CHARACTERS.transform);
        }
        else {
            Destroy(player);
            Instantiate(lightPrefab, player.transform.position, player.transform.rotation, CHARACTERS.transform);
        }
        
        return null;
    }
}

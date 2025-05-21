using UnityEngine;
using System;
using Cinemachine;

public class SavePointScript : MonoBehaviour
{
    public event Action interactingAction;
    private GameObject player;

    [SerializeField] private GameObject CHARACTERS;
    [SerializeField] private GameObject lightPrefab;
    [SerializeField] private GameObject heavyPrefab;
    [SerializeField] private new GameObject camera;
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
            GameObject newPlayer = Instantiate(heavyPrefab, player.transform.position, player.transform.rotation, CHARACTERS.transform);
            camera.GetComponent<CinemachineVirtualCamera>().Follow = newPlayer.transform;
        }
        else {
            Destroy(player);
            GameObject newPlayer = Instantiate(lightPrefab, player.transform.position, player.transform.rotation, CHARACTERS.transform);
            camera.GetComponent<CinemachineVirtualCamera>().Follow = newPlayer.transform;
        }
        
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float health = 100;
    bool isInAir;

    // Getter
    public float getHealth() {
        return health;
    }

    // Setter
    public void setHealth(float h) {
        health = h;
    }

    // Getter
    public bool getIsInAir() {
        return isInAir;
    }
    // Setter
    public void setIsInAir(bool jump) {
        isInAir = jump;
    }

}

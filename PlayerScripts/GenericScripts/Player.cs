using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    static float health = 100;
    static bool isInAir;

    // Getter
    public static float getHealth() {
        return health;
    }

    // Setter
    public static void setHealth(float h) {
        health = h;
    }

    // Getter
    public static bool getIsInAir() {
        return isInAir;
    }
    // Setter
    public static void setIsInAir(bool jump) {
        isInAir = jump;
    }

}

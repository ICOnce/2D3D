using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDets : MonoBehaviour
{

    private int health;
    private int damage;
    public int GetHealth()
    {
        return health;
    }

    public int GetDamage()
    {
        return damage;
    }

    public Vector3 getPos()
    {
        return transform.position;
    }
}

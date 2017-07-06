using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGunStrategy {

    int reloadTime
    {
        get;
    }

    int range
    {
        get;
    }

    int dmg
    {
        get;
    }

    int currentAmmo
    {
        get;
        set;
    }

    int capacity
    {
        get;
    }

    LayerMask mask
    {
        get;
    }

    IEnumerator Reload();

    void setProjectile();

    GameObject getGameObject();
}

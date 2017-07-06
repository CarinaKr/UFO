using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGunStrategy {

    bool isReloading
    {
        get;
    }

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

    IEnumerator ShotEffect();

    void setProjectile();

    GameObject getGameObject();
}

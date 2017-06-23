using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGunStrategy {

    float reloadTime
    {
        get;
    }

    void Reload();

    void setProjectile();

    GameObject getGameObject();
}

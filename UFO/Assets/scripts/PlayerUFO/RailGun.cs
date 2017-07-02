using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : MonoBehaviour, IGunStrategy {

    int _reloadTime;

    public float reloadTime
    {
        get
        {
            return _reloadTime;
        }
    }

    public void Reload()
    {
       
    }

    public void setProjectile()
    {

    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }
}

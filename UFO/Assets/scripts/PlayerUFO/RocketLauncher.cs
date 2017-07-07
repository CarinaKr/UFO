using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IGunStrategy {
    
    int _reloadTime = 5;
    int _dmg = 20;
    int _range = 700;
    int _currentAmmo;
    int _capacity;
    float _firerate;
    bool _isReloading;
    AudioSource _shotSound;

    public LayerMask _mask;

    public IEnumerator Reload()
    {
        throw new NotImplementedException();
    }

    public void setProjectile()
    {
        throw new NotImplementedException();
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public IEnumerator ShotEffect()
    {
        throw new NotImplementedException();
    }


    public int reloadTime
    {
        get
        {
            return _reloadTime;
        }
    }

    public int dmg
    {
        get
        {
            return _dmg;
        }
    }

    public int range
    {
        get
        {
            return _range;
        }
    }

    public LayerMask mask
    {
        get
        {
            return _mask;
        }
    }

    public int capacity
    {
        get
        {
            return _capacity;
        }
    }

    public int currentAmmo
    {
        get
        {
            return _currentAmmo;
        }
        set
        {
            _currentAmmo = value;
        }
    }

    public bool isReloading
    {
        get
        {
            return _isReloading;
        }
    }

    public float firerate
    {
        get
        {
            return _firerate;
        }
    }
}

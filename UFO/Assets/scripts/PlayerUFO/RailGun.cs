using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : MonoBehaviour, IGunStrategy {

    int _reloadTime;
    int _dmg;
    int _range;
    int _currentAmmo;
    int _capacity;

    public LayerMask _mask;
   
    public RailGun()
    {
        _capacity = 100;
        _currentAmmo = _capacity;
        _range = 700;
        _dmg = 5;
        _reloadTime = 5;
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _currentAmmo = _capacity;
        StopCoroutine(Reload());
    }

    public void setProjectile()
    {

    }

    public GameObject getGameObject()
    {
        return this.gameObject;
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
}

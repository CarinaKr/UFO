using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IGunStrategy {

    public float reloadTime
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public void Reload()
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Gun : MonoBehaviour {
    private int cooldown;
    public int maxCooldown;

    public GameObject projectile;

    // Use this for initialization
    void Start()
    {
        cooldown = maxCooldown;
    }

    public void Shoot()
    {
        cooldown--;

        if (Input.GetButton("Fire1"))
        {
            if(cooldown <= 0)
            {
                GameObject bullet = PoolBehaviour.bulletPool.GetObject();

                bullet.transform.position = GameObject.Find("BarrelEnd").transform.position + Vector3.forward;
                cooldown = maxCooldown;
            }
            
        }
    }
}*/

public class Gun
{
    private int cooldown;
    public int maxCooldown;

    public GameObject projectile;

    private IGunStrategy gunStrat;
    private Transform barrelEnd;

    public void Shoot()
    {
        //shoot
        cooldown--;

        if (Input.GetButton("Fire1"))
        {
            if (cooldown <= 0)
            {
                GameObject bullet = PoolBehaviour.bulletPool.GetObject();

                bullet.transform.position = GameObject.Find("BarrelEnd").transform.position + Vector3.forward;
                cooldown = maxCooldown;
            }

        }
    }

    public void Aim()
    {
        //aim
    }
    
    public void setGunStrat(IGunStrategy strat)
    {
        gunStrat = strat;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    private int cooldown;
    private int maxCooldown = 1; //sollte eigentlich jede gun spezifisch haben!

    private IGunStrategy gunStrat;
    private Transform barrelEnd;

    public Gun() { }

    public Gun(IGunStrategy gunStrat, Transform barrelEnd)
    {
        this.gunStrat = gunStrat;
        this.barrelEnd = barrelEnd;
    }

    public void Shoot()
    {
        //shoot
        cooldown--;

        if (Input.GetButton("Fire1"))
        {
            if (cooldown <= 0)
            {
                GameObject bullet = PoolBehaviour.bulletPool.GetObject();

                bullet.transform.position = barrelEnd.transform.position + Vector3.forward;
                cooldown = maxCooldown;
            }

        }
    }

    public void Aim(Transform gun)
    {
        //aim
        
    }
    
    public void setGunStrat(IGunStrategy strat, Transform barrelEnd)
    {
        gunStrat = strat;
        this.barrelEnd = barrelEnd;
    }
}

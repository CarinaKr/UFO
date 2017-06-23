using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    private int cooldown;
    private int maxCooldown = 1; //sollte eigentlich jede gun spezifisch haben!

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
                Debug.Log(maxCooldown);
                GameObject bullet = PoolBehaviour.bulletPool.GetObject();

                bullet.transform.position = barrelEnd.position + Vector3.forward;
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
        Debug.Log("Weapon Switched");
        this.barrelEnd = barrelEnd;
        gunStrat = strat;
    }
}

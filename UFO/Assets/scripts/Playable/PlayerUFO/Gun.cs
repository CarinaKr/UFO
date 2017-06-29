using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    public RectTransform reticle;

    private int cooldown;
    private int maxCooldown = 1; //sollte eigentlich jede gun spezifisch haben!

    private IGunStrategy gunStrat;
    private Transform barrelEnd;
    private Transform[] barrelEnds;

    private Vector3 eulerAngleOffset = new Vector3(90, 0, 0);

    public Gun() { }

    public Gun(IGunStrategy gunStrat, Transform leftBarrelEnd, Transform rightBarrelEnd)
    {
        barrelEnds = new Transform[2];
        this.gunStrat = gunStrat;
        this.barrelEnds[0] = leftBarrelEnd;
        this.barrelEnds[1] = rightBarrelEnd;
        reticle = GameObject.FindGameObjectWithTag("CrossHair").GetComponent<RectTransform>();
    }

    public void Shoot()
    {
        //shoot
        cooldown--;

        if (Input.GetButton("Fire1"))
        {
            if (cooldown <= 0)
            {
                GameObject bulletLeft = PoolBehaviour.bulletPool.GetObject();
                GameObject bulletRight = PoolBehaviour.bulletPool.GetObject();

                bulletLeft.transform.position = barrelEnds[0].transform.position + Vector3.forward;
                bulletRight.transform.position = barrelEnds[1].transform.position + Vector3.forward;
                cooldown = maxCooldown;
            }

        }
    }

    public void Aim()
    {
        //aim
        Debug.Log("Aiming");
        //barrelEnds[0].LookAt(new Vector3(5,5,5));
        for(int i = 0; i <barrelEnds.Length; i++)
        {
            barrelEnds[i].parent.parent.LookAt(reticle);
            barrelEnds[i].parent.parent.Rotate(eulerAngleOffset, Space.Self);
        }

    }

    public void setGunStrat(IGunStrategy strat, Transform leftBarrelEnd, Transform rightBarrelEnd)
    {
        gunStrat = strat;
        barrelEnds[0] = leftBarrelEnd;
        barrelEnds[1] = rightBarrelEnd;
    }
}

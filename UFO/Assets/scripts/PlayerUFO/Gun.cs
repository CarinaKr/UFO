using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun 
{
    private RectTransform reticle;
    private Transform cameraTransform;
    
    private int cooldown;
    private int maxCooldown = 18; //sollte eigentlich jede gun spezifisch haben!

    private IGunStrategy gunStrat;
    private Transform barrelEnd;
    private Transform[] barrelEnds;

    private Vector3 eulerAngleOffset = new Vector3(90, 0, 0);
    private Vector3 target;

    public Gun() { }

    public Gun(IGunStrategy gunStrat, Transform leftBarrelEnd, Transform rightBarrelEnd)
    {
        barrelEnds = new Transform[2];
        this.gunStrat = gunStrat;
        this.barrelEnds[0] = leftBarrelEnd;
        this.barrelEnds[1] = rightBarrelEnd;
        reticle = GameObject.FindGameObjectWithTag("CrossHair").GetComponent<RectTransform>();
        cameraTransform = Camera.main.GetComponent<Transform>();
    }

    public void Shoot()
    {
        //shoot
        cooldown--;

        if (Input.GetButton("Fire1") && gunStrat.currentAmmo > 0 && !gunStrat.isReloading)
        {
            if (cooldown <= 0)
            {
                //take bullets from the pool
                GameObject bulletLeft = PoolBehaviour.bulletPool.GetObject();
                GameObject bulletRight = PoolBehaviour.bulletPool.GetObject();
                
                //and put them in the right place
                bulletLeft.transform.position = barrelEnds[0].transform.position + Vector3.forward;
                bulletRight.transform.position = barrelEnds[1].transform.position + Vector3.forward;

                //give them their target
                bulletLeft.GetComponent<BulletMovement>().setDir(target);
                bulletRight.GetComponent<BulletMovement>().setDir(target);

                //set the properties of the bullet according to the gun
                bulletLeft.GetComponent<BulletMovement>().dmg = gunStrat.dmg;
                bulletRight.GetComponent<BulletMovement>().dmg = gunStrat.dmg;
                //reduce Ammo
                gunStrat.currentAmmo -= 2;

                //cooldown reset
                cooldown = maxCooldown;
            }

        }
    }
    
    public void Aim()
    {
        //aim
        for(int i = 0; i <barrelEnds.Length; i++)
        {
            barrelEnds[i].parent.parent.LookAt(reticle);
            barrelEnds[i].parent.parent.Rotate(eulerAngleOffset, Space.Self);
        }

        Vector3 direction = -1 * (cameraTransform.position - reticle.position);
        RaycastHit hit;

        if(Physics.Raycast(cameraTransform.position, direction.normalized, out hit, gunStrat.range, gunStrat.mask)) {
            target = hit.point;
        }
        else
        {
            target = direction.normalized * gunStrat.range;
        }

    }

    public void setGunStrat(IGunStrategy strat, Transform leftBarrelEnd, Transform rightBarrelEnd)
    {
        gunStrat = strat;
        barrelEnds[0] = leftBarrelEnd;
        barrelEnds[1] = rightBarrelEnd;
    }
}

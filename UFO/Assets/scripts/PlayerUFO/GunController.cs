using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public RectTransform reticle;
    public Text ammoText;
    public Text curWeapon;
    public Image curWeaponImg;

    private Gun gun;
    private const int AMOUNT_OF_GUNS= 2;
    private int currentWeaponIndex, nextWeaponIndex;

    private int maxCooldown = 18;
    private int cooldown;

    private Transform[,] guns;
    private List<IGunStrategy> strategies = new List<IGunStrategy>();


    // Use this for initialization
    void Start()
    {
        fillStrategies();
        gun = new Gun(strategies[0], this.guns[nextWeaponIndex, 0], this.guns[nextWeaponIndex, 1]);
        ammoText.text = "Ammo: " + strategies[nextWeaponIndex].currentAmmo + " / " + strategies[nextWeaponIndex].capacity;
        curWeapon.text = "" + strategies[nextWeaponIndex].gunName;
        curWeaponImg.material = strategies[nextWeaponIndex].img;
    }

    // Update is called once per frame
    void Update()
    {
        gun.Aim();
        if (Input.GetButtonDown("Fire2"))
        {
            currentWeaponIndex = nextWeaponIndex;
            nextWeaponIndex++;
            nextWeaponIndex %= strategies.Count;
            gun.setGunStrat(strategies[nextWeaponIndex], this.guns[nextWeaponIndex, 0], this.guns[nextWeaponIndex, 1]);
            strategies[nextWeaponIndex].getGameObject().SetActive(true);
            strategies[currentWeaponIndex].getGameObject().SetActive(false);
            curWeapon.text = strategies[nextWeaponIndex].gunName;
            curWeaponImg.material = strategies[nextWeaponIndex].img;
        }
        if(Input.GetButtonDown("Reload"))
        {
            if (!strategies[nextWeaponIndex].isReloading)
            {
                ammoText.gameObject.SetActive(false);
                StartCoroutine(strategies[nextWeaponIndex].Reload());
            }
        }

        if(!strategies[nextWeaponIndex].isReloading && !ammoText.gameObject.activeSelf)
        {
            ammoText.gameObject.SetActive(true);
            ammoText.text = "Ammo: " + strategies[nextWeaponIndex].currentAmmo + " / " + strategies[nextWeaponIndex].capacity;
        }

        cooldown--;
        if (Input.GetButton("Fire1") && strategies[nextWeaponIndex].currentAmmo > 0 && !strategies[nextWeaponIndex].isReloading)
        {
            //cooldown time has to be zero to be able to shoot
            if (cooldown <= 0)
            {
                gun.Shoot();
                ammoText.text = "Ammo: " + strategies[nextWeaponIndex].currentAmmo + " / " + strategies[nextWeaponIndex].capacity;
                StartCoroutine(strategies[nextWeaponIndex].ShotEffect());

                cooldown = maxCooldown;
            }
        }
        //reset cooldown time when releasing fire button
        //else if(Input.GetButtonUp("Fire1"))
        //{
        //    cooldown = 0;
        //}
    }

    void FixedUpdate()
    {
        //if (Input.GetButton("Fire1") && strategies[nextWeaponIndex].currentAmmo > 0 && !strategies[nextWeaponIndex].isReloading)
        //{
        //    cooldown--;
        //    if(cooldown<=0)
        //    {
        //        gun.Shoot();
        //        ammoText.text = "Ammo: " + strategies[nextWeaponIndex].currentAmmo + " / "+ strategies[nextWeaponIndex].capacity;
        //        StartCoroutine(strategies[nextWeaponIndex].ShotEffect());

        //        cooldown = maxCooldown;
        //    }
            
        //}
    }

    void fillStrategies()
    {
        foreach (IGunStrategy strat in GetComponentsInChildren<IGunStrategy>())
        {
            strategies.Add(strat);
            strat.getGameObject().SetActive(false);
        }

        Helper.FindComponentsInChildrenWithTag<Transform>(gameObject, "barrelEnd");
        List<Transform> transf = new List<Transform>();
        //There we go: Add every component from generic static method into a list of transforms. This List contains the barrelEnds with which we wanna shoot. 
        foreach (Component c in Helper.masterList)
        {
            if(c.GetType() == typeof(Transform))
            {
                transf.Add(c.transform);
            }
        }

        guns = new Transform[strategies.Count, 2];
        int count = 0;
        for(int i = 0; i < strategies.Count; i++)
        {
            for(int j = 0; j < AMOUNT_OF_GUNS; j++)
            {
                guns[i, j] = Helper.masterList[count].transform;
                count++;
            }
        }
        strategies[0].getGameObject().SetActive(true);
    }
}

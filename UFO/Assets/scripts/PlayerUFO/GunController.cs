using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public RectTransform reticle;

    private Gun gun;
    private const int AMOUNT_OF_GUNS= 2;
    private int currentWeaponIndex, nextWeaponIndex;

    private Transform[,] guns;
    private List<IGunStrategy> strategies = new List<IGunStrategy>();

    // Use this for initialization
    void Start()
    {
        fillStrategies();
        gun = new Gun(strategies[0], this.guns[nextWeaponIndex, 0], this.guns[nextWeaponIndex, 1]);
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
        }
    }

    void FixedUpdate()
    {
        gun.Shoot();
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
                Debug.Log("Transform: " + c.gameObject.ToString());
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
                Debug.Log("In Array: " + guns[i, j].ToString());
                count++;
            }
        }
        strategies[0].getGameObject().SetActive(true);
    }
}

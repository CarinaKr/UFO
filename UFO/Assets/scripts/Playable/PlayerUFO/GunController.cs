using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    private Gun gun;
    private int currentWeaponIndex, nextWeaponIndex;

    public Transform[] barrelEnds;
    public Transform[,] guns;
    private List<IGunStrategy> strategies = new List<IGunStrategy>();

    private GameObject[,] gunArray;

    // Use this for initialization
    void Start () {
        fillStrategies();
        gun = new Gun(strategies[0], barrelEnds[0]);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire2"))
        {
            currentWeaponIndex = nextWeaponIndex;
            nextWeaponIndex++;
            nextWeaponIndex %= strategies.Count;
            gun.setGunStrat(strategies[nextWeaponIndex], this.barrelEnds[nextWeaponIndex]);
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
        gunArray = new GameObject[GetComponentsInChildren<IGunStrategy>().Length, 2];

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            
        }
        foreach (IGunStrategy strat in GetComponentsInChildren<IGunStrategy>())
        {
            strategies.Add(strat);
            strat.getGameObject().SetActive(false);
        }

        strategies[0].getGameObject().SetActive(true);

        guns = new Transform[strategies.Count, 2];
        /*
        for(int i = 0; i < strategies.Count; i++)
        {
            guns[i] = strategies[i].getGameObject();
            for(int j = 0; j < )
        }*/


        for(int i = 0; i < strategies.Count; i++)
        {
            Debug.Log(strategies[i].ToString());
            guns[i,0] = strategies[i].getGameObject().transform;
            Debug.Log(guns[i,0].ToString());
        }
    }
}

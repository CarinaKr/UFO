using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    private Gun gun;
    private int currentWeaponIndex, nextWeaponIndex;

    public Transform[] barrelEnds;
    private List<IGunStrategy> strategies = new List<IGunStrategy>();

    // Use this for initialization
    void Start () {
        fillStrategies();

        gun = new Gun();
        gun.setGunStrat(strategies[nextWeaponIndex], barrelEnds[nextWeaponIndex]);
        
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump"))
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
        foreach (IGunStrategy strat in GetComponentsInChildren<IGunStrategy>())
        {
            strategies.Add(strat);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    private Gun gun;
    private int nextWeaponIndex;

    public Transform[] barrelEnds;
    private IGunStrategy[] strategies = new IGunStrategy[2];

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
            Debug.Log("Pressed");
            IGunStrategy strategy = GetComponentInChildren<RocketLauncher>();
           //Transform barrelEnd = GetComponentInChildren<RocketLauncher>(); //get the transform of correct barrelEnd here and pass it into setGunStrat
            //gun.setGunStrat(strategy);

            nextWeaponIndex++;
            nextWeaponIndex %= strategies.Length;
            gun.setGunStrat(strategies[nextWeaponIndex], this.barrelEnds[nextWeaponIndex]);

        }
	}

    void FixedUpdate()
    {
        gun.Shoot();
    }

    void fillStrategies()
    {
        strategies[0] = GetComponentInChildren<RailGun>();
        strategies[1] = GetComponentInChildren<RocketLauncher>();
    }
}

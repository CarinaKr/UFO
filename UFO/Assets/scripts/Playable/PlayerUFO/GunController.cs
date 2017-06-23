using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    private Gun gun;

	// Use this for initialization
	void Start () {
        gun = new Gun();
        gun.setGunStrat(GetComponentInChildren<RailGun>());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Pressed");
            IGunStrategy strategy = GetComponentInChildren<RocketLauncher>();
            Transform barrelEnd = GetComponentInChildren<RocketLauncher>(); //get the transform of correct barrelEnd here and pass it into setGunStrat
            gun.setGunStrat(strategy);
        }
	}

    void FixedUpdate()
    {
        gun.Shoot();
    }
}

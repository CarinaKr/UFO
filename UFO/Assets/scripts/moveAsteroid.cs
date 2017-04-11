using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAsteroid : MonoBehaviour {

    private Vector3 zRichtung;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            Debug.Log("trigger exit");
            zRichtung = other.transform.position - transform.position;
            transform.Translate(zRichtung *2,Space.World);
        }
    }
}

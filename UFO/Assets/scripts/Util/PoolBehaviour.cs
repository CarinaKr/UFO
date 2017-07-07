using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBehaviour : MonoBehaviour {

    public static PoolManager bulletPool;
    public GameObject bulletObj;
    public int bulletPoolSize;

	// Use this for initialization
	void Awake () {
        bulletPool = new PoolManager(bulletPoolSize, bulletObj);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

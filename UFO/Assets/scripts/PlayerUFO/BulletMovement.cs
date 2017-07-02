using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float moveSpeed;
    public int lifeTicks;

    private int aliveFor;

	void Awake () {
        transform.parent = GameObject.Find("BulletContainer").transform;
	}
	
	
	void FixedUpdate () {
        transform.Translate(transform.forward * moveSpeed);

        aliveFor++;
        if(aliveFor == lifeTicks)
        {
            aliveFor = 0;
            PoolBehaviour.bulletPool.ReleaseObject(gameObject);
        }
	}
}

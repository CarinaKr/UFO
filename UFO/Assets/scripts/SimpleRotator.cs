using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour {
    private float x;
    float zRot;
    float oldZ;
    float diff;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(x == 50 )
        {
            zRot = Random.Range(-1, 1);
             x = 0;
             diff = oldZ - zRot;
        }
       // Debug.Log(diff);
       
        transform.Rotate(Vector3.up, diff*100*Time.deltaTime, Space.Self);
        oldZ = zRot;
        x++;
	}
}

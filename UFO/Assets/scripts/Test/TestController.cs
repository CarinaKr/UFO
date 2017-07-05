using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {

    public float moveSpeed = 1f;
    public Transform center;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.E))
        {
            transform.RotateAround(center.position, Vector3.up, 15f);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.RotateAround(center.position, Vector3.up, -15f);
        }


        transform.Translate(movement);


    }
}

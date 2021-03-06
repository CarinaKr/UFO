﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitController : MonoBehaviour {

    // Rigidbody rb;

    public Transform rotator;
    public float maxTiltX;
    public float maxTiltZ;
    public float moveSpeed;
    public float turnSpeed;
    public Transform centerPoint;

    private float xRot = 0;
    private float zRot = 0;
    private float xMove = 0;
    private float zMove = 0;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //save last rotation
        float oldXRot = xRot;
        float oldZRot = zRot;

        //Controller Input
        //xRot = Input.GetAxis("Vertical") * maxTiltX;
        zRot = Input.GetAxis("Horizontal") * maxTiltZ;

        //Calc diff between last and current rotation
        float diffX = (xRot - oldXRot) * 1;
        float diffZ = (zRot - oldZRot) * 1;
        
        //actually rotate
        rotator.Rotate(Vector3.left, diffX, Space.World);
        transform.Rotate(Vector3.up, diffZ, Space.Self);

        //rotation to movement
        xMove = zRot * moveSpeed * Time.deltaTime * turnSpeed;
        zMove = xRot * moveSpeed * Time.deltaTime;

        rotator.RotateAround(centerPoint.position, Vector3.up, xMove);
        //move the object (parent to move everything)
        
        //rotator.Translate(xMove, 0, zMove, Space.World);
    }

    void FixedUpdate()
    {

    }
}

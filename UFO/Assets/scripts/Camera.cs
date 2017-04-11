using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform ziel;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - ziel.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = ziel.position + offset;
	}
}

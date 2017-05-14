using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = player.position - gameObject.transform.position;	
	}

	void LateUpdate () {
        gameObject.transform.position = player.position - offset;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour {

    public float moveSpeed;
    public float maxTiltX, maxTiltZ;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float tiltX = Input.GetAxis("Vertical") * maxTiltX;
        float tiltZ = Input.GetAxis("Horizontal") * maxTiltZ;

        transform.localEulerAngles = new Vector3(tiltX, 0, -1*tiltZ);


        float z = tiltX * Time.deltaTime * moveSpeed;
        float x = tiltZ * Time.deltaTime * moveSpeed;

        transform.Translate(x, 0, z, Space.World);

    }
}

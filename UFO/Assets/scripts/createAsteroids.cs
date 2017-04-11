using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createAsteroids : MonoBehaviour {

    public GameObject[] asteroids;
    
    public int asteroidsNumber;
    public float minSize, maxSize;

    private float sphereSize;
    private float x, y, z,xRot,yRot,zRot;
    private float size;
    private GameObject asteroid;

	// Use this for initialization
	void Start () {

        sphereSize = GetComponent<SphereCollider>().radius;

        for (int i = 0; i < asteroidsNumber; i++)
        {
            x = Random.Range(-sphereSize , sphereSize )+transform.position.x;
            y = Random.Range(-sphereSize , sphereSize )+transform.position.y;
            z = Random.Range(-sphereSize , sphereSize )+transform.position.z;

            xRot = Random.Range(0, 360);
            yRot = Random.Range(0, 360);
            zRot = Random.Range(0, 360);

            size = Random.Range(minSize, maxSize);

            asteroid = asteroids[Random.Range(0, asteroids.Length)];

            Instantiate(asteroid, new Vector3(x, y,z), Quaternion.Euler(xRot,yRot,zRot));
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

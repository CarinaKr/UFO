using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createAsteroids : MonoBehaviour {

    public GameObject[] asteroids;
    public Transform ufo;
    
    public int asteroidsNumber;
    public float minSize, maxSize;
    public float radius;

    private float sphereSize;
    private float x, y, z,xRot,yRot,zRot;
    private float size;
    private GameObject asteroid;

	// Use this for initialization
	void Start () {

        sphereSize = radius;

        for (int i = 0; i < asteroidsNumber; i++)
        {
            x = Random.Range(-sphereSize, sphereSize);
            y = Random.Range(0, sphereSize / 2);
            z = Random.Range(-sphereSize, sphereSize);

            xRot = Random.Range(0, 360);
            yRot = Random.Range(0, 360);
            zRot = Random.Range(0, 360);

            size = Random.Range(minSize, maxSize);

            asteroid = asteroids[Random.Range(0, asteroids.Length)];

            if (asteroid != null)
            {
                asteroid.transform.localScale = new Vector3(size, size, size);
                GameObject.Instantiate(asteroid, new Vector3(x, y, z), Quaternion.Euler(xRot, yRot, zRot), gameObject.transform);
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        
	}
}

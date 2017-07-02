using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createAsteroids : MonoBehaviour {

    public GameObject[] asteroids;
    public Transform ufo;

    public int circlesNumber;
    public int asteroidsNumber;
    public float minSize, maxSize;
    public float radius;
    public float minY, maxY;

    private float sphereSize;
    private float x, y, z,xRot,yRot,zRot;
    private float size;
    private GameObject asteroid;

	// Use this for initialization
	void Start () {

        sphereSize = radius;
        for(int j=0;j<circlesNumber;j++)
        {
               for (int i = 0; i < asteroidsNumber; i++)
                {
                    x = Random.Range(-sphereSize, sphereSize);
                    y = Random.Range(minY,maxY);
                    z = Random.Range(-sphereSize, sphereSize);

                    xRot = Random.Range(0, 360);
                    yRot = Random.Range(0, 360);
                    zRot = Random.Range(0, 360);

                    size = Random.Range(minSize, maxSize);

                    asteroid = asteroids[Random.Range(0, asteroids.Length)];

                    if (asteroid != null)
                    {                                               //Ring aus Asteroiden am Rand des Radius:
                        Vector3 direction = new Vector3(x, 0,z);    //Richtung in xz-Ebene
                        direction.Normalize();                      //Normalenvektor der Richtung
                        direction *= radius-(j*(radius/circlesNumber));                        //und mit dem Radius multiplizieren
                        direction.y = y;                           //zufällige Höhe setzen
                        asteroid.transform.localScale = new Vector3(size, size, size);
                        GameObject.Instantiate(asteroid, direction, Quaternion.Euler(xRot, yRot, zRot), gameObject.transform);
                    }
                }
        }
       

	}
	
	// Update is called once per frame
	void Update () {
        
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAsteroid : MonoBehaviour {

    public Transform center;
    public Transform ufo;
    public float moveSpeed;
    public float radius;

    public int damage;

    private Vector3 zZiel;


	// Use this for initialization
	void Start () {
        zZiel = new Vector3(center.position.x,transform.position.y, center.position.z);
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position,zZiel, moveSpeed*Time.deltaTime);
        if(transform.position==zZiel)
        {
            Reposition();
        }
	}

    public void Reposition()
    {
        Vector3 direction = new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
        direction.Normalize();
        transform.Translate(direction * radius, Space.World);
        //in Blickrichtung des Ufos an den Rand des Radius bewegen.
        //transform.Rotate(Vector3.up, ufo.transform.rotation.y);
        //transform.Translate(Vector3.forward* radius);
    }

    public int getDamage()
    {
        return damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            transform.position = zZiel;
        }
    }

    
}

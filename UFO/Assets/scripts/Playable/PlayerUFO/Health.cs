using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int health;
    public GameObject screen;
    public GameObject healthTop;
    public Material[] healthColor;

    private int asteroidDamage;
    private float healthFull;
    private int maxHealth;
    

	// Use this for initialization
	void Start () {
        healthFull = healthTop.transform.localScale.z;
        maxHealth = health;
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Asteroid")
        {
            health -= other.GetComponent<moveAsteroid>().getDamage();
            float healthSize= (health / maxHealth) * healthFull;
            healthTop.transform.localScale=new Vector3(healthTop.transform.localScale.x, healthTop.transform.localScale.y,healthSize);
            if(healthSize<0.6)
            {
                Material[] mats = healthTop.GetComponent<Renderer>().materials;
                mats[1] = healthColor[1];
                healthTop.GetComponent<Renderer>().materials = mats;
            }
            else if(healthSize<0.3)
            {
                Material[] mats = healthTop.GetComponent<Renderer>().materials;
                mats[1] = healthColor[2];
                healthTop.GetComponent<Renderer>().materials = mats;
            }

        }

        if(health<=0)
        {
            this.gameOver();
        }
    }

    public void gameOver()
    {
        screen.gameObject.SetActive(true);
        gameObject.GetComponent<ArduinoTest>().enabled=false;
        gameObject.GetComponent<CockpitController>().enabled = false;
    }
	
}

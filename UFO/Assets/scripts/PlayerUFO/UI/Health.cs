using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health;
    public GameObject screen;
    public Image healthTop;
    public Material[] healthColor;

    private int asteroidDamage;
    private float healthFull;
    private float maxHealth;

    private bool isCollided = false;
    

	// Use this for initialization
	void Start () {
        healthFull = healthTop.transform.localScale.z;
        maxHealth = health;
    }

    void update()
    {
        isCollided = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Asteroid")
        {
            if (isCollided)
            {
                return;
            }
            isCollided = true;
            health -= other.GetComponent<moveAsteroid>().getDamage();
            float healthPercent = health / maxHealth;
            healthTop.fillAmount = healthPercent;
            if (healthPercent < 0.3)
            {
                Material[] mats = healthTop.GetComponent<Renderer>().materials;
                mats[0] = healthColor[2];
                healthTop.GetComponent<Renderer>().materials = mats;
            }
            else if (healthPercent<0.6)
            {
                Material[] mats = healthTop.GetComponent<Renderer>().materials;
                mats[0] = healthColor[1];
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

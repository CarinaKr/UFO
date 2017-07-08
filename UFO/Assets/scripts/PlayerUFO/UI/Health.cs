using System.Collections;
using System.Collections.Generic;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField]
    private VRCameraFade m_CameraFade;
    public float health;
    public GameObject screen;
    public Image healthTop;
    public Material[] healthColor;
    public int gameOverLevelNumber;

    private int asteroidDamage;
    private float healthFull;
    private float maxHealth;

    private bool isCollided = false;
    

	// Use this for initialization
	void Start () {
        maxHealth = health;
    }

    void Update()
    {
        isCollided = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Asteroid")
        {
            moveAsteroid asteroid = other.GetComponent<moveAsteroid>();
            if (isCollided||asteroid.tagged)
            {
                return;
            }
            isCollided = true;
            asteroid.tagged = true;
            health -= asteroid.getDamage();
            float healthPercent = health / maxHealth;
            healthTop.fillAmount = healthPercent;
            if (healthPercent < 0.3)
            {
                //Material[] mats = healthTop.GetComponent<Renderer>().materials;
                //mats[0] = healthColor[2];
                //healthTop.GetComponent<Renderer>().materials = mats;
                healthTop.material = healthColor[2];
            }
            else if (healthPercent<0.6)
            {
                //Material[] mats = healthTop.GetComponent<Renderer>().materials;
                //mats[0] = healthColor[1];
                //healthTop.GetComponent<Renderer>().materials = mats;
                healthTop.material = healthColor[1];
            }
            

        }

        if(health<=0)
        {
            StartCoroutine(gameOver());
        }
    }

    public IEnumerator gameOver()
    {
        // If the camera is already fading, ignore.
        if (m_CameraFade.IsFading)
            yield break;

        // Wait for the camera to fade out.
        yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));

        // Load the level.
        SceneManager.LoadScene(gameOverLevelNumber, LoadSceneMode.Single);
    }
	
}

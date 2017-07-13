using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour {

    public ParticleSystem hitParticles;
    public ParticleSystem explosionParticles;

    private int lifepoints = 100;
    private int score = 20;

    private MeshRenderer mesh;
    private Color standardColor;

    void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        standardColor = mesh.material.color;
        hitParticles = GetComponentInChildren<ParticleSystem>();
        //explosionParticles = GetComponentInChildren<ParticleSystem>();
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("coll");

        if (col.gameObject.CompareTag("bullet"))
        {
            bool isRocket = col.gameObject.GetComponent<BulletMovement>().isRocket;
            Debug.Log("col");
            ReceiveDamage(col.gameObject.GetComponent<BulletMovement>().dmg, col.contacts[0].point, isRocket);
            col.gameObject.GetComponent<BulletMovement>().isShot = false;
            if(isRocket)
                PoolBehaviour.rocketPool.ReleaseObject(col.gameObject);
            else
                PoolBehaviour.bulletPool.ReleaseObject(col.gameObject);
        }
    }

    //void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("Trigger");

    //    if (col.gameObject.CompareTag("bullet"))
    //    {
    //        Debug.Log("col");
    //        ReceiveDamage(col.gameObject.GetComponent<BulletMovement>().dmg, col.transform.position);
    //        col.gameObject.GetComponent<BulletMovement>().isShot = false;
    //        PoolBehaviour.bulletPool.ReleaseObject(col.gameObject);
    //    }
    //}

    public void ReceiveDamage(int damageTake, Vector3 hitPoint, bool isRocket)
    {
        lifepoints -= damageTake;
        StartCoroutine(ShowDamageEffect(hitPoint, isRocket));
        if(lifepoints <= 0)
        {
            lifepoints = 100;
            ScoreManager.increaseScore(score);
            gameObject.GetComponent<moveAsteroid>().Reposition();
        }
    }

    IEnumerator ShowDamageEffect(Vector3 hitPoint, bool isRocket)
    {
        if(isRocket)
        {
            explosionParticles.transform.position = hitPoint;
            explosionParticles.transform.localScale = new Vector3();
            explosionParticles.Play();
        }
        Debug.Log(lifepoints);
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        mesh.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mesh.material.color = standardColor;
        yield return new WaitForSeconds(0.5f);
        mesh.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mesh.material.color = standardColor;
        yield return new WaitForSeconds(0.5f);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour {

    public ParticleSystem hitParticles;

    private int lifepoints = 100;
    private int score = 20;

    private MeshRenderer mesh;
    private Color standardColor;

    void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        standardColor = mesh.material.color;
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("coll");
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger");

        if (col.gameObject.CompareTag("bullet"))
        {
            Debug.Log("col");
            ReceiveDamage(col.gameObject.GetComponent<BulletMovement>().dmg, col.transform.position);
            col.gameObject.GetComponent<BulletMovement>().isShot = false;
            PoolBehaviour.bulletPool.ReleaseObject(col.gameObject);
        }
    }

    public void ReceiveDamage(int damageTake, Vector3 hitPoint)
    {
        lifepoints -= damageTake;
        StartCoroutine(ShowDamageEffect(hitPoint));
        if(lifepoints <= 0)
        {
            lifepoints = 100;
            ScoreManager.increaseScore(score);
            gameObject.GetComponent<moveAsteroid>().Reposition();
        }
    }

    IEnumerator ShowDamageEffect(Vector3 hitPoint)
    {
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

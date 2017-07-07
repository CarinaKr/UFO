using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour {

    public ParticleSystem hitParticles;

    private int lifepoints = 100;

    private MeshRenderer mesh;
    private Color standardColor;

    void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        standardColor = mesh.material.color;
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }


    public void ReceiveDamage(int damageTake, Vector3 hitPoint)
    {
        lifepoints -= damageTake;
        StartCoroutine(ShowDamageEffect(hitPoint));
        if(lifepoints <= 0)
        {
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

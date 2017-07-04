using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour {

    private int lifepoints = 100;

    private MeshRenderer mesh;
    private Color standardColor;

    void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        standardColor = mesh.material.color;
    }


    public void ReceiveDamage(int damageTake)
    {
        lifepoints -= damageTake;
        StartCoroutine(ShowDamageEffect());
        if(lifepoints <= 0)
        {
            gameObject.GetComponent<moveAsteroid>().Reposition();
        }
    }

    IEnumerator ShowDamageEffect()
    {
        Debug.Log("Playing Effect");
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

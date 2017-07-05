using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float moveSpeed;
    public int lifeTicks;

    private int dmg = 2;
    private Vector3 targetPosition;
    private int aliveFor;

    void Awake() {
        transform.parent = GameObject.Find("BulletContainer").transform;
    }


    void OnEnable()
    {
        targetPosition = GameObject.FindGameObjectWithTag("CrossHair").GetComponent<RectTransform>().position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
        //transform.Translate(Vector3.up, Space.Self);
        //rb.velocity = targetPosition;


        aliveFor++;
        if (aliveFor == lifeTicks)
        {
            aliveFor = 0;
            PoolBehaviour.bulletPool.ReleaseObject(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Asteroid"))
        {
            col.gameObject.GetComponent<AsteroidHealth>().ReceiveDamage(dmg);
        }

        aliveFor = 0;
        PoolBehaviour.bulletPool.ReleaseObject(gameObject);
    }

    //public void setDir(Vector3 targetPosition)
    //{
    //    this.targetPosition = targetPosition;
    //}
}

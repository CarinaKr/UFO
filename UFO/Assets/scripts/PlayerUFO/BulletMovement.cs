﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float moveSpeed;
    public int lifeTicks;
    public bool _isRocket;
    public ParticleSystem rocketTrail;
    
    private int _dmg;
    private Vector3 targetPosition;
    private Vector3 eulerAngleOffset = new Vector3(0, 0, 0);
    private int aliveFor;
    private bool _isShot;
    //private Animator rocketAC;

    void Awake() {
        transform.parent = _isRocket ? GameObject.Find("RocketContainer").transform : GameObject.Find("BulletContainer").transform;
        rocketTrail = GetComponentInChildren<ParticleSystem>();
        //rocketAC = GetComponent<Animator>();
    }


    void OnEnable()
    {
        targetPosition = GameObject.FindGameObjectWithTag("CrossHair").GetComponent<RectTransform>().position;
        transform.LookAt(targetPosition);
        isShot = true;
    }

    void OnDisable()
    {
        isShot = false;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
        if (transform.position == targetPosition)
        {
            if(_isRocket)
            {

                PoolBehaviour.rocketPool.ReleaseObject(gameObject);
            }
            else
            {
                PoolBehaviour.bulletPool.ReleaseObject(gameObject);
            }
        }
    }

    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.CompareTag("Asteroid"))
    //    {
    //        Debug.Log("col");
    //        col.gameObject.GetComponent<AsteroidHealth>().ReceiveDamage(dmg, col.contacts[0].point);
    //    }

    //    aliveFor = 0;
    //    isShot = false;
    //    PoolBehaviour.bulletPool.ReleaseObject(gameObject);
    //}
    
    public void setDir(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public int dmg
    {
        get
        {
            return _dmg;
        }
        set
        {
            _dmg = value;
        }
    }

    public bool isShot
    {
        get;
        set;
    }

    public bool isRocket
    {
        get
        {
            return _isRocket;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float moveSpeed;
    public int lifeTicks;

    private int _dmg;
    private Vector3 targetPosition;
    private Vector3 eulerAngleOffset = new Vector3(0, 0, 0);
    private int aliveFor;
    private bool _isShot;

    void Awake() {
        transform.parent = GameObject.Find("BulletContainer").transform;
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
}

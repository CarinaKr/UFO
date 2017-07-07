using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : MonoBehaviour, IGunStrategy {

    int _reloadTime;
    int _dmg;
    int _range;
    int _currentAmmo;
    int _capacity;
    float _firerate;
    bool _isReloading;
    BulletMovement[] _bulletList;
    AudioSource _shotSound;
    
    public LayerMask _mask;
    public GameObject bulletContainer;
    public GameObject muzzleFlashPrefab;
    public Transform leftBarrel;
    public Transform rightBarrel;
    public AudioClip gunShotSound;

    void Start()
    {
        //_firerate = 
        _capacity = 100;
        _currentAmmo = _capacity;
        _range = 500;
        _dmg = 5;
        _reloadTime = 5;
        _isReloading = false;
        _bulletList = new BulletMovement[PoolBehaviour.bulletPool.objects.Length];
        for (int i = 0; i < PoolBehaviour.bulletPool.objects.Length; i++)
        {
            _bulletList[i] = PoolBehaviour.bulletPool.objects[i].GetComponent<BulletMovement>();
        }
        _shotSound = GetComponent<AudioSource>();
    }

    public IEnumerator ShotEffect()
    {
        if(!_shotSound.isPlaying)
        {
            //_shotSound.Play();
            _shotSound.PlayOneShot(gunShotSound);
        }
        GameObject leftMuzzleFlash = Instantiate(muzzleFlashPrefab, leftBarrel);
        GameObject rightMuzzleFlash = Instantiate(muzzleFlashPrefab, rightBarrel);
        yield return new WaitForSeconds(0.015f);
        Destroy(leftMuzzleFlash);
        Destroy(rightMuzzleFlash);
        StopCoroutine(ShotEffect());
    }

    public IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        _currentAmmo = _capacity;
        _isReloading = false;
        foreach(BulletMovement bullet in _bulletList)
        {
            if (bullet.isShot)
            {
                bullet.isShot = false;
                PoolBehaviour.bulletPool.ReleaseObject(bullet.gameObject);
            }
        }
        StopCoroutine(Reload());
    }

    public void setProjectile()
    {

    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public int reloadTime
    {
        get
        {
            return _reloadTime;
        }
    }

    public int dmg
    {
        get
        {
            return _dmg;
        }
    }

    public int range
    {
        get
        {
            return _range;
        }
    }

    public LayerMask mask
    {
        get
        {
            return _mask;
        }
    }

    public int capacity
    {
        get
        {
            return _capacity;
        }
    }

    public int currentAmmo
    {
        get
        {
            return _currentAmmo;
        }
        set
        {
            _currentAmmo = value;
        }
    }

    public bool isReloading
    {
        get
        {
            return _isReloading;
        }
    }

    public float firerate
    {
        get
        {
            return _firerate;
        }
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailGun : MonoBehaviour, IGunStrategy {

    string _name;
    float _cooldown;
    int _reloadTime;
    int _dmg;
    int _range;
    int _currentAmmo;
    int _capacity;
    float _firerate;
    bool _isReloading;
    BulletMovement[] _bulletList;
    AudioSource _shotSound;
    Animator[] animators;
    
    public LayerMask _mask;
    public GameObject bulletContainer;
    public GameObject muzzleFlashPrefab;
    public Transform leftBarrel;
    public Transform rightBarrel;
    public AudioClip gunShotSound;
    public Material _img;
    public Text _reloadText;

    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        _name = "Rail Gun";
        _cooldown = 0.5f;
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
        Animate(true);
        if (!_shotSound.isPlaying)
        {
            //_shotSound.Play();
            _shotSound.PlayOneShot(gunShotSound);
        }
        GameObject leftMuzzleFlash = Instantiate(muzzleFlashPrefab, leftBarrel);
        GameObject rightMuzzleFlash = Instantiate(muzzleFlashPrefab, rightBarrel);
        Destroy(leftMuzzleFlash, 0.1f);
        Destroy(rightMuzzleFlash, 0.1f);
        yield return new WaitForSeconds(0.5f);
        Animate(false);
        StopCoroutine(ShotEffect());
    }

    public IEnumerator Reload()
    {
        _isReloading = true;
        _reloadText.gameObject.SetActive(true);
        for(int i = 0; i < 100; i++)
        {
            _reloadText.text = "Reloading... " + i + "%"; 
            yield return new WaitForSeconds((float)_reloadTime/100);
        }
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
        _reloadText.gameObject.SetActive(false);
        StopCoroutine(Reload());
    }

    void Animate(bool isShooting)
    {
        Debug.Log("Shooting: " + isShooting);
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetBool("isShooting", isShooting);
        }
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

    public Material img
    {
        get
        {
            return _img;
        }
    }

    public string gunName
    {
        get
        {
            return _name;
        }
    }

    public float cooldown
    {
        get
        {
            return _cooldown;
        }
    }
}

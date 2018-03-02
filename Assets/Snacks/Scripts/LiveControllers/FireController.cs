using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

    [Header("Debug")]
    [SerializeField] private bool _canShoot;

    [Header("Fire Stats")]
    [SerializeField] private float _shooFireRate;

    public void Firing()
    {
        _canShoot = false;
        Invoke("EnableShoot", _shooFireRate);
    }

    public void EnableShoot()
    {
        _canShoot = true;
    }

    public bool GetCanShoot() { return _canShoot; }
}

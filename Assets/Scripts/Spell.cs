﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {
    private Rigidbody2D mRigidbody;
    [SerializeField]
    private float speed = 0;
    private Transform target;

    // Start is called before the first frame update
    void Start () {
        mRigidbody = GetComponent<Rigidbody2D> ();
        target = GameObject.Find ("Target").transform;
        Debug.Log (target.position);
        Debug.Log (target.transform.position);
    }

    public void Fire () {

    }

    private void FixedUpdate () {
        Vector2 direction = target.position - transform.position;
        mRigidbody.velocity = direction.normalized * speed;
        float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update () {

    }
}
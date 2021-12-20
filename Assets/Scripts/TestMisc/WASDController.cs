using System;
using System.Collections;
using System.Collections.Generic;
using CompositeRigidbody;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    public float speed = 100;
    private CompositeRigidbody.CompositeRigidbody body;

    private void Start()
    {
        body = GetComponent<CompositeRigidbody.CompositeRigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.body.AddForce(new Vector3(Input.GetAxis("Horizontal") * speed, 0,  Input.GetAxis("Vertical") * speed));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabbable : Selectable
{
    private Rigidbody rb;
    private Collider coll;
    private Transform follow;

    public bool Selected = false;

    private Vector3 lastPos;
    private bool deselectedFlag = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (follow != null)
        {
            transform.position = follow.position;
            transform.rotation = follow.rotation;
        }

        if (deselectedFlag)
        {
            rb.velocity = (transform.position - lastPos).normalized * Time.deltaTime;
            deselectedFlag = false;
        }

        lastPos = transform.position;
    }

    public override void Select(Selector selector)
    {
        base.Select(selector);

        rb.useGravity = false;
        rb.isKinematic = true;
        coll.isTrigger = true;

        follow = selector.transform;

        if (selector.HandModelSwitch != null)
            selector.HandModelSwitch.SetHideStatus(true);

        Selected = true;
    }

    public override void Deselect(Selector selector)
    {
        base.Deselect(selector);

        follow = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        coll.isTrigger = false;

        if (selector.HandModelSwitch != null)
            selector.HandModelSwitch.SetHideStatus(false);

        Selected = false;
        deselectedFlag = true;
    }

    
}

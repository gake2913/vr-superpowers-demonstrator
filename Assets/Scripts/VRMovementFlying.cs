using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRMovementFlying : MonoBehaviour
{

    public InputActionReference MoveAxis2D;
    public InputActionReference GripPress;
    public InputActionReference TriggerPress;
    public Transform YellowRoomRoot;
    public float Speed = 2f;
    public Rigidbody Rigidbody;
    public Transform Direction;

    public AnimSpeedTest AnimSpeedTest;

    public Transform Head;
    public Collider BodyCollider;
    public Collider HeadCollider;

    public VRSelectorDistance selectorDistance;
    public VRSelectorGrab selectorGrab;

    public VRHandModelSwitch[] handSwitcher;

    private VRMovement vrMovement;
    private bool flyingActive = false;
    private bool switchBackAllowed = false;

    private Transform playerParent;
    private Vector3 playerPos;
    private Quaternion playerRot;

    private Transform fly;

    // Start is called before the first frame update
    void Start()
    {
        vrMovement = GetComponent<VRMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!flyingActive) return;

        if ((GetTriggerPressValue(GripPress) || GetTriggerPressValue(TriggerPress)) && switchBackAllowed)
        {
            Deactivate();
        }

        if (!switchBackAllowed && !GetTriggerPressValue(GripPress) && !GetTriggerPressValue(TriggerPress)) switchBackAllowed = true;

        Vector2 vel = MoveAxis2D.action.ReadValue<Vector2>() * Speed;
        Vector3 vel3 = Direction.forward * vel.y + Direction.right * vel.x;
        Rigidbody.velocity = vel3;

        AnimSpeedTest.AnimationSpeed = vel3.magnitude;
    }

    public void Activate(Transform fly)
    {
        flyingActive = true;
        switchBackAllowed = false;
        vrMovement.enabled = false;
        Rigidbody.useGravity = false;

        HeadCollider.enabled = true;
        BodyCollider.enabled = false;

        selectorGrab.ClearHovering();
        selectorGrab.enabled = false;
        selectorDistance.enabled = false;

        foreach (VRHandModelSwitch s in handSwitcher) s.ChangeModel(0, false);

        playerParent = transform.parent;
        playerPos = transform.position;
        playerRot = transform.rotation;

        transform.position = fly.position - Head.localPosition;
        this.fly = fly;
        fly.gameObject.SetActive(false);
    }

    public void Deactivate()
    {
        if (Mathf.Abs(transform.position.x - YellowRoomRoot.position.x) > 5) return;
        if (Mathf.Abs(transform.position.y - YellowRoomRoot.position.y) > 10) return;
        if (Mathf.Abs(transform.position.z - YellowRoomRoot.position.z) > 5) return;

        flyingActive = false;
        vrMovement.enabled = true;
        Rigidbody.useGravity = true;

        HeadCollider.enabled = false;
        BodyCollider.enabled = true;

        selectorDistance.enabled = true;
        selectorGrab.enabled = true;
        selectorGrab.ClearHovering();

        foreach (VRHandModelSwitch s in handSwitcher) s.ChangeModel(-1, false);

        fly.gameObject.SetActive(true);
        fly.transform.position = Head.position;
        fly.transform.rotation = Head.rotation;

        transform.parent = playerParent;
        transform.position = playerPos;
        transform.rotation = playerRot;

        
        fly.GetComponent<VRFly>().ExitFlyMode();
        fly.GetComponent<VRFly>().HoverExit(null);
    }

    Type lastActiveType = null;

    bool GetTriggerPressValue(InputActionReference input)
    {
        Type typeToUse = null;

        if (input.action.activeControl != null)
        {
            typeToUse = input.action.activeControl.valueType;
        }
        else
        {
            typeToUse = lastActiveType;
        }

        if (typeToUse == typeof(bool))
        {
            lastActiveType = typeof(bool);
            bool value = input.action.ReadValue<bool>();
            return value;
        }
        else if (typeToUse == typeof(float))
        {
            lastActiveType = typeof(float);
            float value = input.action.ReadValue<float>();
            return value > 0.5f;
        }

        return false;
    }
}

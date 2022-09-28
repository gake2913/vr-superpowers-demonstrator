using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMovementFlying : MonoBehaviour
{

    public InputActionReference MoveAxis2D;
    public InputActionReference GripPress;
    public InputActionReference TriggerPress;
    public Transform YellowRoomRoot;
    public float Speed = 2f;
    public CharacterController CharacterController;
    public Transform Direction;

    public XRRayInteractor selectorDistance;
    public XRDirectInteractor selectorGrab;

    public VRHandModelSwitch[] handSwitcher;

    public XROrigin XROrigin;

    public GameObject FlyVision;

    public Animator FadeToBlack;

    public ActionBasedContinuousMoveProvider vrMovement;
    public CharacterControllerDriver characterControllerDriver;

    private bool flyingActive = false;
    private bool switchBackAllowed = false;

    private Transform playerParent;
    private Vector3 playerPos;
    private Quaternion playerRot;
    private float colliderSize;

    private Transform fly;

    // Start is called before the first frame update
    void Start()
    {
        
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
        CharacterController.Move(vel3 * Time.deltaTime);

        if (vel.magnitude != 0) CharacterController.center = XROrigin.CameraInOriginSpacePos;
    }

    public void Activate(Transform fly)
    {
        flyingActive = true;
        switchBackAllowed = false;
        vrMovement.enabled = false;
        characterControllerDriver.enabled = false;
        //CharacterController.useGravity = false;

        CharacterController.height = 0;

        FlyVision.SetActive(true);

        selectorGrab.enabled = false;
        selectorDistance.enabled = false;

        foreach (VRHandModelSwitch s in handSwitcher) s.ChangeModel(0, false);

        playerParent = transform.parent;
        playerPos = transform.position;
        playerRot = transform.rotation;
        colliderSize = CharacterController.radius;
        CharacterController.radius = 0.1f;

        transform.position = fly.position - XROrigin.CameraInOriginSpacePos;
        this.fly = fly;
        fly.gameObject.SetActive(false);
    }

    public void Deactivate()
    {
        //Debug.Log("Deactivate");

        if (Mathf.Abs(transform.position.x - YellowRoomRoot.position.x) > 5) return;
        if (Mathf.Abs(transform.position.y - YellowRoomRoot.position.y) > 10) return;
        if (Mathf.Abs(transform.position.z - YellowRoomRoot.position.z) > 5) return;

        if (!PowersManager.instance.YellowActive) return;

        //Debug.Log("De 2");

        FadeToBlack.SetTrigger("Fade");
        StartCoroutine(DeactivateCoroutine());
    }

    private IEnumerator DeactivateCoroutine()
    {
        flyingActive = false;

        yield return new WaitForSeconds(0.5f);

        
        vrMovement.enabled = true;
        characterControllerDriver.enabled = true;
        //CharacterController.useGravity = true;

        FlyVision.SetActive(false);

        selectorDistance.enabled = true;
        selectorGrab.enabled = true;

        foreach (VRHandModelSwitch s in handSwitcher) s.ChangeModel(-1, false);

        fly.gameObject.SetActive(true);
        fly.transform.position = XROrigin.Camera.transform.position;
        fly.transform.rotation = XROrigin.Camera.transform.rotation;

        transform.parent = playerParent;
        transform.position = playerPos;
        transform.rotation = playerRot;
        CharacterController.radius = colliderSize;

        fly.GetComponent<VRFly>().ExitFlyMode();
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

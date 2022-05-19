using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRMovement : MonoBehaviour
{

    public InputActionReference MoveAxis2D;
    public float Speed = 2f;
    public Rigidbody Rigidbody;
    public Transform Direction;

    public AnimSpeedTest AnimSpeedTest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = MoveAxis2D.action.ReadValue<Vector2>() * Speed;
        Vector3 vel3 = Direction.forward * vel.y + Direction.right * vel.x;
        vel3.y = Rigidbody.velocity.y;
        Rigidbody.velocity = vel3;

        AnimSpeedTest.AnimationSpeed = vel3.magnitude;
    }
}

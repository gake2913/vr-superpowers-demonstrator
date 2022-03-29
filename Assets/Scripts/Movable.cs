using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movable : Selectable
{

    public bool Selected = false;

    private Transform follow;
    private Rigidbody rb;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        if (!Selected) return;

        /*Vector3 velocity = follow.position - transform.position;
        velocity = velocity / Time.fixedDeltaTime;
        velocity = Vector3.ClampMagnitude(velocity, 50);

        Vector3 angular = follow.rotation.eulerAngles - transform.rotation.eulerAngles;
        angular = angular / Time.fixedDeltaTime;

        rb.velocity = velocity;
        rb.angularVelocity = angular;*/
    }

    public override void Select(Selector selector)
    {
        base.Select(selector);

        Selected = true;
        GameObject target = new GameObject();
        target.transform.position = transform.position;
        target.transform.rotation = transform.rotation;
        target.transform.parent = selector.transform;
        AttachJoint(target);
        follow = target.transform;

        rb.useGravity = false;
    }

    public override void Deselect(Selector selector)
    {
        base.Deselect(selector);

        Selected = false;
        Destroy(follow.gameObject);

        rb.useGravity = true;
    }

    private void AttachJoint(GameObject target)
    {
        Rigidbody newRb = target.AddComponent<Rigidbody>();
        newRb.isKinematic = true;

        ConfigurableJoint joint = target.AddComponent<ConfigurableJoint>();
        joint.connectedBody = rb;
        joint.configuredInWorldSpace = true;
        joint.xDrive = NewJointDrive(600, 6);
        joint.yDrive = NewJointDrive(600, 6);
        joint.zDrive = NewJointDrive(600, 6);
        joint.slerpDrive = NewJointDrive(600, 6);
        joint.rotationDriveMode = RotationDriveMode.Slerp;
    }

    private JointDrive NewJointDrive(float force, float damping)
    {
        JointDrive drive = new JointDrive();
        //drive.mode = JointDriveMode.Position;
        drive.positionSpring = force;
        drive.positionDamper = damping;
        drive.maximumForce = Mathf.Infinity;
        return drive;
    }
}

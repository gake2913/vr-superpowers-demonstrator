using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracelet : MonoBehaviour
{

    public bool PowerActive = false;

    public Collider RoomCollider;

    public PowerColors Color;

    public Transform DebugBall;
    public Transform DebugBounds;

    private bool onSocket = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PowerActive = true;
        if (onSocket)
        {
            if (!InsideRoom()) PowerActive = false;
        }
        else
        {
            PowerActive = false;
        }

        PowersManager.instance.PowerSet(Color, PowerActive);
        animator.SetBool("InsideRoom", InsideRoom());
    }

    public void PutOn()
    {
        onSocket = true;
    }

    public void TakeOff()
    {
        onSocket = false;
    }

    private bool InsideRoom()
    {
        Vector3 vecFromRoomCenter = transform.position - RoomCollider.transform.position;
        Vector3 aaVecFromRoomCenter = Quaternion.Inverse(RoomCollider.transform.rotation) * vecFromRoomCenter;
        Vector3 aaGlobalVector = aaVecFromRoomCenter + RoomCollider.transform.position;

        if (DebugBall != null)
            DebugBall.position = aaGlobalVector;

        if (DebugBounds != null)
        {
            DebugBounds.position = RoomCollider.bounds.center;
            DebugBounds.localScale = RoomCollider.bounds.size;
        }

        Vector3 closest = RoomCollider.ClosestPoint(transform.position);
        return closest == transform.position;

        if (RoomCollider.bounds.Contains(aaGlobalVector)) return true;


        return false;
    }
}

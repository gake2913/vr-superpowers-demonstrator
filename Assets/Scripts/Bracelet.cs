using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracelet : MonoBehaviour
{

    public bool PowerActive = false;

    public Transform RoomRoot;
    public Vector3 RoomLimits;

    public PowerColors Color;

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
        if (Mathf.Abs(transform.position.y - RoomRoot.position.y) > RoomLimits.y) return false;
        if (Mathf.Abs(transform.position.x - RoomRoot.position.x) > RoomLimits.x) return false;
        if (Mathf.Abs(transform.position.z - RoomRoot.position.z) > RoomLimits.z) return false;
        return true;
    }
}

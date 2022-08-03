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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PowerActive = true;
        if (onSocket)
        {
            if (Mathf.Abs(transform.position.y - RoomRoot.position.y) > RoomLimits.y) PowerActive = false;
            if (Mathf.Abs(transform.position.x - RoomRoot.position.x) > RoomLimits.x) PowerActive = false;
            if (Mathf.Abs(transform.position.z - RoomRoot.position.z) > RoomLimits.z) PowerActive = false;
        }
        else
        {
            PowerActive = false;
        }

        PowersManager.instance.PowerSet(Color, PowerActive);
    }

    public void PutOn()
    {
        onSocket = true;
    }

    public void TakeOff()
    {
        onSocket = false;
    }
}

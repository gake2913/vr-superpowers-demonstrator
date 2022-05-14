using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRColliderTracker : MonoBehaviour
{

    public Transform Camera;
    public bool MoveToZero = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.transform.localPosition;
        if (MoveToZero) pos.y = 0;
        transform.localPosition = pos;
    }
}

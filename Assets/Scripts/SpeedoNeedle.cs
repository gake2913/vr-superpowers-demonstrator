using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedoNeedle : MonoBehaviour
{

    public float MaxAngle = 135;

    public AnimSpeedTest AnimSpeedTest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, MaxAngle - MaxAngle * 2f * AnimSpeedTest.AnimationSpeed);
    }
}

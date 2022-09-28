using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDistanceAnimationSpeed : MonoBehaviour
{

    public Transform RightHand;
    public Transform Body;

    public float MinDistance = 0.2f;
    public float MaxDistance = 0.7f;

    private AnimSpeedTest animSpeedTest;

    // Start is called before the first frame update
    void Start()
    {
        animSpeedTest = GetComponent<AnimSpeedTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PowersManager.instance.GreenActive) 
            animSpeedTest.AnimationSpeed = 0.5f;
        else
        {
            animSpeedTest.AnimationSpeed = ((RightHand.position - Body.position).magnitude - MinDistance) / (MaxDistance - MinDistance);
        }
    }
}

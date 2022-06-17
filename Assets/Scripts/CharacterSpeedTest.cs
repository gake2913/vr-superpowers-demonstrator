using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpeedTest : MonoBehaviour
{

    public CharacterController CharacterController;

    private AnimSpeedTest animSpeedTest;

    // Start is called before the first frame update
    void Start()
    {
        animSpeedTest = GetComponent<AnimSpeedTest>();
    }

    // Update is called once per frame
    void Update()
    {
        animSpeedTest.AnimationSpeed = CharacterController.velocity.magnitude;
    }
}

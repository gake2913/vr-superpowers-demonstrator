using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SuperpowerParticles : MonoBehaviour
{

    public XROrigin XROrigin;

    [Space()]
    public Transform RoomRed;
    public Transform RoomYellow;
    public Transform RoomGreen;
    public Transform RoomBlue;
    public Transform RoomTeal;

    [Space()]
    public Color ColorRed;
    public Color ColorYellow;
    public Color ColorGreen;
    public Color ColorBlue;
    public Color ColorTeal;

    [Space()]
    public ParticleSystem ParticleSystem;

    private int currentRoom = -1;
    private int lastRoom = -1;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRoom = -1;

        if (Mathf.Abs(XROrigin.Camera.transform.position.x - RoomRed.position.x) < 10)
        {
            if (Mathf.Abs(XROrigin.Camera.transform.position.y - RoomRed.position.y) < 20)
            {
                if (Mathf.Abs(XROrigin.Camera.transform.position.z - RoomRed.position.z) < 10) currentRoom = 0;
            }
        }

        if (Mathf.Abs(XROrigin.Camera.transform.position.x - RoomYellow.position.x) < 5)
        {
            if (Mathf.Abs(XROrigin.Camera.transform.position.y - RoomYellow.position.y) < 10)
            {
                if (Mathf.Abs(XROrigin.Camera.transform.position.z - RoomYellow.position.z) < 5) currentRoom = 1;
            }
        }

        if (Mathf.Abs(XROrigin.Camera.transform.position.x - RoomGreen.position.x) < 5)
        {
            if (Mathf.Abs(XROrigin.Camera.transform.position.y - RoomGreen.position.y) < 10)
            {
                if (Mathf.Abs(XROrigin.Camera.transform.position.z - RoomGreen.position.z) < 5) currentRoom = 2;
            }
        }

        if (Mathf.Abs(XROrigin.Camera.transform.position.x - RoomBlue.position.x) < 5)
        {
            if (Mathf.Abs(XROrigin.Camera.transform.position.y - RoomBlue.position.y) < 10)
            {
                if (Mathf.Abs(XROrigin.Camera.transform.position.z - RoomBlue.position.z) < 5) currentRoom = 3;
            }
        }

        if (Mathf.Abs(XROrigin.Camera.transform.position.x - RoomTeal.position.x) < 5)
        {
            if (Mathf.Abs(XROrigin.Camera.transform.position.y - RoomTeal.position.y) < 10)
            {
                if (Mathf.Abs(XROrigin.Camera.transform.position.z - RoomTeal.position.z) < 5) currentRoom = 4;
            }
        }

        if (currentRoom != -1 && lastRoom == -1)
        {
            var main = ParticleSystem.main;
            switch (currentRoom)
            {
                case 0: main.startColor = ColorRed; break;
                case 1: main.startColor = ColorYellow; break;
                case 2: main.startColor = ColorGreen; break;
                case 3: main.startColor = ColorBlue; break;
                case 4: main.startColor = ColorTeal; break;
            }

            ParticleSystem.Play();
        }

        if (currentRoom == -1 && lastRoom != -1)
        {
            ParticleSystem.Stop();
        }

        lastRoom = currentRoom;
    }
}
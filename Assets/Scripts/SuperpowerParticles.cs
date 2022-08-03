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
    [ColorUsage(true, true)] public Color ColorRed;
    [ColorUsage(true, true)] public Color ColorYellow;
    [ColorUsage(true, true)] public Color ColorGreen;
    [ColorUsage(true, true)] public Color ColorBlue;
    [ColorUsage(true, true)] public Color ColorTeal;

    [Space()]
    public ParticleSystem ParticleSystem;
    public Material ParticleMaterial;

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

        if (PowersManager.instance.RedActive) currentRoom = 0;
        if (PowersManager.instance.YellowActive) currentRoom = 1;
        if (PowersManager.instance.GreenActive) currentRoom = 2;
        if (PowersManager.instance.BlueActive) currentRoom = 3;
        if (PowersManager.instance.TealActive) currentRoom = 4;

        if (currentRoom != -1 && lastRoom == -1)
        {
            var main = ParticleSystem.main;
            switch (currentRoom)
            {
                case 0: main.startColor = ColorRed; ParticleMaterial.SetColor("_EmissionColor", ColorRed); break;
                case 1: main.startColor = ColorYellow; ParticleMaterial.SetColor("_EmissionColor", ColorYellow); break;
                case 2: main.startColor = ColorGreen; ParticleMaterial.SetColor("_EmissionColor", ColorGreen); break;
                case 3: main.startColor = ColorBlue; ParticleMaterial.SetColor("_EmissionColor", ColorBlue); break;
                case 4: main.startColor = ColorTeal; ParticleMaterial.SetColor("_EmissionColor", ColorTeal); break;
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

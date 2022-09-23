using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnlyGrabWithPower : MonoBehaviour
{

    public PowerColors Color;

    private XRBaseInteractable grab;

    // Start is called before the first frame update
    void Start()
    {
        grab = GetComponent<XRBaseInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        grab.enabled = PowersManager.instance.GetPowerActive(Color);
    }
}

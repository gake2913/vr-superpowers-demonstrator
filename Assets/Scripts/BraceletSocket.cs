using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BraceletSocket : MonoBehaviour
{

    private XRSocketInteractor socket;
    private Bracelet selected;

    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutOnBracelet()
    {
        selected = ((XRGrabInteractable)(socket.firstInteractableSelected)).GetComponent<Bracelet>();
        selected.PutOn();
    }

    public void TakeOffBracelet()
    {
        selected.TakeOff();
        selected = null;
    }
}

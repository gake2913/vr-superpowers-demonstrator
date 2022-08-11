using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRAirWriter : MonoBehaviour
{

    public InputActionReference TriggerPress;
    public float TriggerMin = 0.1f;
    public float TriggerMax = 0.8f;
    public GameObject WritingParentPrefab;
    public Transform WritingOrigin;
    public Transform RedRoomRoot;
    public float Thickness = 0.05f;
    public float ThicknessMin = 0.02f;
    public float ThicknessMax = 0.2f;
    public bool WritingActive = false;

    private Transform currentParent;

    private bool lastWritingActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - RedRoomRoot.position.x) > 10) return;
        if (Mathf.Abs(transform.position.y - RedRoomRoot.position.y) > 10) return;
        if (Mathf.Abs(transform.position.z - RedRoomRoot.position.z) > 10) return;

        if (!PowersManager.instance.RedActive) return;

        float triggerValue = TriggerPress.action.ReadValue<float>();
        WritingActive = triggerValue > TriggerMin;
        float TriggerMod = (triggerValue - TriggerMin) / (TriggerMax - TriggerMin);
        Thickness = ThicknessMin + (ThicknessMax - ThicknessMin) * TriggerMod;

        if (WritingActive && !lastWritingActive)
        {
            GameObject parent = Instantiate(WritingParentPrefab);
            parent.transform.position = WritingOrigin.position;
            currentParent = parent.transform;
            lastWritingActive = true;
        }

        if (WritingActive)
        {
            currentParent.position = WritingOrigin.position;
            //Debug.Log(Thickness);
            currentParent.GetComponent<AirWriterControl>().UpdateThickness(Thickness);
        }

        if (!WritingActive && lastWritingActive)
        {
            currentParent = null;
        }

        lastWritingActive = WritingActive;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class VRSelectorGrab : Selector
{

    public InputActionReference GripPress;

    private bool lastTriggerPress = false;

    private List<Selectable> hoveringSelectables = new List<Selectable>();
    private SphereCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        coll.radius = Range;

        bool triggerPressed = GetTriggerPressValue();

        float dist = float.MaxValue;
        Selectable closest = null;
        foreach (Selectable selectable in hoveringSelectables)
        {
            float distance = Mathf.Abs(Vector3.Distance(transform.position, selectable.transform.position));
            if (distance < dist)
            {
                closest = selectable;
                dist = distance;
            }
        }

        if (closest != null)
        {
            closest.Highlight = true;

            if (lastHighlight != null && closest != lastHighlight)
            {
                lastHighlight.Highlight = false;
                lastHighlight = closest;
                closest.HoverEnter(this);
            }
            else if (lastHighlight == null)
            {
                lastHighlight = closest;
                closest.HoverEnter(this);
            }

            if (triggerPressed && !lastTriggerPress)
            {
                closest.Select(this);
                lastClick = closest;
            }
        }
        else
        {
            if (lastHighlight != null)
            {
                lastHighlight.Highlight = false;
                lastHighlight.HoverExit(this);
                lastHighlight = null;
            }
        }

        if (!triggerPressed && lastTriggerPress && lastClick != null)
        {
            lastClick.Deselect(this);
            lastClick = null;
        }

        lastTriggerPress = triggerPressed;
    }

    Type lastActiveType = null;

    bool GetTriggerPressValue()
    {
        Type typeToUse = null;

        if (GripPress.action.activeControl != null)
        {
            typeToUse = GripPress.action.activeControl.valueType;
        }
        else
        {
            typeToUse = lastActiveType;
        }

        if (typeToUse == typeof(bool))
        {
            lastActiveType = typeof(bool);
            bool value = GripPress.action.ReadValue<bool>();
            return value;
        }
        else if (typeToUse == typeof(float))
        {
            lastActiveType = typeof(float);
            float value = GripPress.action.ReadValue<float>();
            return value > 0.5f;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Selectable>(out Selectable selectable))
        {
            hoveringSelectables.Add(selectable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Selectable>(out Selectable selectable))
        {
            hoveringSelectables.Remove(selectable);
        }
    }

    public void ClearHovering()
    {
        hoveringSelectables.Clear();
    }
}

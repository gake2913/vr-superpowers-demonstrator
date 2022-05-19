using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRSelectorDistance : Selector
{

    public InputActionReference TriggerAxis;
    public InputActionReference TriggerPress;
    public LineRenderer LineRenderer;

    private bool lastTriggerPress = false;

    // Start is called before the first frame update
    void Start()
    {
        LineRenderer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool selectorActive = TriggerAxis.action.ReadValue<float>() > 0.1f;
        bool triggerPressed = GetTriggerPressValue();

        LineRenderer.gameObject.SetActive(selectorActive);
        LineRenderer.SetPosition(1, Vector3.forward * Range);

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin, direction);

        RaycastHit[] hits = Physics.RaycastAll(ray, Range);
        List<Selectable> selectables = new List<Selectable>();
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent<Selectable>(out Selectable selectable) && selectorActive)
            {
                selectables.Add(selectable);
            }
        }

        float dist = float.MaxValue;
        Selectable closest = null;
        foreach (Selectable selectable in selectables)
        {
            float distance = Mathf.Abs(Vector3.Distance(origin, selectable.transform.position));
            if (distance < dist)
            {
                closest = selectable;
                dist = distance;
            }
        }

        if (closest != null)
        {
            LineRenderer.SetPosition(1, Vector3.forward * dist);

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

        if (TriggerPress.action.activeControl != null)
        {
            typeToUse = TriggerPress.action.activeControl.valueType;
        }
        else
        {
            typeToUse = lastActiveType;
        }

        if (typeToUse == typeof(bool))
        {
            lastActiveType = typeof(bool);
            bool value = TriggerPress.action.ReadValue<bool>();
            return value;
        }
        else if (typeToUse == typeof(float))
        {
            lastActiveType = typeof(float);
            float value = TriggerPress.action.ReadValue<float>();
            return value > 0.5f;
        }

        return false;
    }

    private void OnDisable()
    {
        LineRenderer.gameObject.SetActive(true);
        LineRenderer.SetPosition(1, Vector3.forward * 0.2f);
    }
}

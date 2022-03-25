using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

    [Range(0, 10)] public float Range = 5;

    private Selectable lastHighlight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin, direction);

        RaycastHit[] hits = Physics.RaycastAll(ray, Range);
        List<Selectable> selectables = new List<Selectable>();
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent<Selectable>(out Selectable selectable))
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
            closest.Highlight = true;

            if (lastHighlight != null && closest != lastHighlight)
            {
                lastHighlight.Highlight = false;
                lastHighlight = closest;
                closest.HoverEnter();
            }
            else if (lastHighlight == null)
            {
                lastHighlight = closest;
                closest.HoverEnter();
            }

            if (Input.GetMouseButtonDown(0))
            {
                closest.Select();
            }
        }
        else
        {
            if (lastHighlight != null)
            {
                lastHighlight.Highlight = false;
                lastHighlight.HoverExit();
                lastHighlight = null;
            }
        }
    }
}

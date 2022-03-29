using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableReference : Selectable
{

    public Selectable Referencing;

    private new void Update()
    {
        Referencing.ReferenceHighlight(Highlight);
        outline.enabled = false;
    }

    public override void Select(Selector selector)
    {
        Referencing.Select(selector);
    }

    public override void Deselect(Selector selector)
    {
        Referencing.Deselect(selector);
    }

    public override void HoverEnter(Selector selector)
    {
        Referencing.HoverEnter(selector);
    }

    public override void HoverExit(Selector selector)
    {
        Referencing.HoverExit(selector);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{

    public bool Highlight = false;
    public Color OutlineColor = Color.red;
    [Range(0, 10)] public float OutlineWidth = 7f;

    protected Outline outline;

    private List<bool> highlights;

    // Start is called before the first frame update
    public void Start()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineColor = OutlineColor;
        outline.OutlineWidth = OutlineWidth;

        highlights = new List<bool>();
    }

    // Update is called once per frame
    public void Update()
    {
        outline.enabled = Highlight;
    }

    private void LateUpdate()
    {
        if (highlights.Count > 0)
        {
            Highlight = false;
            foreach (bool h in highlights)
            {
                if (h)
                {
                    Highlight = true;
                    break;
                }
            }

            highlights.Clear();
        }
    }

    public virtual void Select(Selector selector)
    {
        Debug.Log("Selected " + gameObject.name);
    }

    public virtual void Deselect(Selector selector)
    {
        Debug.Log("Deselected " + gameObject.name);
    }

    public virtual void HoverEnter(Selector selector)
    {
        Debug.Log("Hover Enter " + gameObject.name);
    }

    public virtual void HoverExit(Selector selector)
    {
        Debug.Log("Hover Exit " + gameObject.name);
    }

    public void ReferenceHighlight(bool highlight)
    {
        highlights.Add(highlight);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{

    public bool Highlight = false;
    public Color OutlineColor = Color.red;
    [Range(0, 10)] public float OutlineWidth = 7f;

    private Outline outline;

    // Start is called before the first frame update
    public void Start()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineColor = OutlineColor;
        outline.OutlineWidth = OutlineWidth;
    }

    // Update is called once per frame
    public void Update()
    {
        outline.enabled = Highlight;
    }

    public virtual void Select()
    {
        Debug.Log("Selected " + gameObject.name);
    }
}

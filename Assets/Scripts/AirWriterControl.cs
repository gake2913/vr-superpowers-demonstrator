using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWriterControl : MonoBehaviour
{

    private struct ThicknessTuple
    {
        public float length;
        public float thickness;
    }

    private List<ThicknessTuple> Thickness = new List<ThicknessTuple>();
    private float overallLength = 0;

    private TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateThickness(float thickness)
    {
        Vector3[] positions = new Vector3[trail.positionCount];
        int numPos = trail.GetPositions(positions);

        float length = 0;
        for (int i = 1; i < numPos; i++)
        {
            length += Vector3.Distance(positions[i - 1], positions[i]);
        }

        if (length > overallLength) overallLength = length;
        bool found = false;
        for (int i = 0; i < Thickness.Count; i++)
        {
            ThicknessTuple t = Thickness[i];
            if (t.length == length)
            {
                found = true;
                t.thickness = thickness;
            }
        }
        if (!found)
        {
            ThicknessTuple t;
            t.length = length;
            t.thickness = thickness;
            Thickness.Add(t);
        }

        AnimationCurve width = new AnimationCurve();
        foreach(ThicknessTuple t in Thickness)
        {
            width.AddKey(1f - (t.length / overallLength), t.thickness);
        }
        width.postWrapMode = WrapMode.ClampForever;
        width.preWrapMode = WrapMode.ClampForever;
        trail.widthCurve = width;

        trail.widthMultiplier = 1;
    }
}

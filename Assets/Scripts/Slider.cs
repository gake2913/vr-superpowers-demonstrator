using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slider : MonoBehaviour
{

    public Vector3 Range;

    public UnityEvent<float> OnSliderChange;

    private Vector3 startPos;
    private Vector3 lastPos;
    private Vector3 minPos;

    private float rangeScale;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        lastPos = startPos;

        minPos = startPos - Range;

        rangeScale = (Range * 2f).magnitude;

        OnSliderChange.Invoke(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != lastPos)
        {
            Vector3 fromMin = transform.position - minPos;
            float value = fromMin.magnitude / rangeScale;

            OnSliderChange.Invoke(value);

            lastPos = transform.position;
        }
    }
}

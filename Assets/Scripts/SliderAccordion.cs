using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SliderAccordion : MonoBehaviour
{

    private float Range;

    public Transform OtherSide;
    public Transform Middle;
    public UnityEvent<float> OnSliderChange;

    private float startPos;
    private float lastPos;
    private float minPos;

    private float rangeScale;

    // Start is called before the first frame update
    void Start()
    {
        startPos = Vector3.Distance(transform.position, OtherSide.position);
        lastPos = startPos;
        Range = startPos;

        minPos = 0.1f;

        rangeScale = Range * 2f;

        OnSliderChange.Invoke(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float pos = Vector3.Distance(transform.position, OtherSide.position);
        if (pos != lastPos)
        {
            float fromMin = pos - minPos;
            float value = fromMin / rangeScale;

            if (PowersManager.instance.RedActive)
                OnSliderChange.Invoke(value);

            lastPos = pos;

            Middle.position = transform.position + (OtherSide.position - transform.position) / 2f;
            Middle.rotation = transform.rotation;

            Vector3 middleScale = Middle.localScale;
            middleScale.x = fromMin;
            Middle.localScale = middleScale;
        }
    }
}

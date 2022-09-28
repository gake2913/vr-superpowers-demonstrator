using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScaler : MonoBehaviour
{

    public Vector3 ScaleEnd, PosEnd;
    [Range(0,1)] public float RoomScale;

    private Vector3 scaleStart, posStart;

    // Start is called before the first frame update
    void Start()
    {
        scaleStart = transform.localScale;
        posStart = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(scaleStart, ScaleEnd, RoomScale);
        transform.localPosition = Vector3.Lerp(posStart, PosEnd, RoomScale);
    }

    public void SetScale(float value)
    {
        RoomScale = value;
    }
}

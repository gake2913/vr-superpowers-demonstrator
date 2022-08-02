using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SizeChange : MonoBehaviour
{

    public LineRenderer LineRenderer;
    public ActionBasedContinuousMoveProvider Move;

    private float lineWidth;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        lineWidth = LineRenderer.widthMultiplier;
        speed = Move.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSize(float height)
    {
        float scale = height / 1.8f;

        transform.localScale = new Vector3(scale, scale, scale);
        LineRenderer.widthMultiplier = scale * lineWidth;
        Move.moveSpeed = scale * speed;
    }
}

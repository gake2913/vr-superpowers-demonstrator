using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeedTest : MonoBehaviour
{

    [Range(-2, 2)] public float AnimationSpeed = 1;

    public Transform TrainLoco;
    public Transform[] TrainCarts;
    public Spline2DComponent SplineComponent;

    public float CartDistance = 0.1f;

    private float splinePos = 0;
    private float splineLength;

    private Dictionary<Transform, Vector3> lastPositions;

    // Start is called before the first frame update
    void Start()
    {
        splineLength = SplineComponent.Length;

        lastPositions = new Dictionary<Transform, Vector3>();

        TrainLoco.position = SplineComponent.InterpolateDistanceWorldSpace(splineMod(0));
        lastPositions[TrainLoco] = TrainLoco.position;
        for (int i = 0; i < TrainCarts.Length; i++)
        {
            TrainCarts[i].position = SplineComponent.InterpolateDistanceWorldSpace(splineMod(i + 1));
            lastPositions[TrainCarts[i]] = TrainCarts[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        splinePos += AnimationSpeed * Time.deltaTime;

        PlaceCart(TrainLoco, splineMod(0));
        for (int i = 0; i < TrainCarts.Length; i++)
        {
            PlaceCart(TrainCarts[i], splineMod(i + 1));
        }
    }

    private void PlaceCart(Transform cart, float splinePos)
    {
        cart.position = SplineComponent.InterpolateDistanceWorldSpace(splinePos);

        Vector3 travelDirection = cart.position - lastPositions[cart];
        if (AnimationSpeed < 0) travelDirection *= -1;
        Quaternion rot = travelDirection == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(travelDirection, transform.up);
        cart.rotation = rot;
        lastPositions[cart] = cart.position;
    }

    private float splineMod(int cart)
    {
        float pos = splinePos - CartDistance * cart;
        pos %= splineLength;

        if (pos < 0) pos += splineLength;
        return pos;
    }

    public void Reset()
    {
        AnimationSpeed = 1;
    }
}

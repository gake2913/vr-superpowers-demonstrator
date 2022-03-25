using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{

    public bool Idle = true;
    public float IdleRange = 0.1f;
    public float TargetIntervall = 0.1f;

    private bool lastIdle = true;
    private Vector3 targetPosition;
    private Vector3 centerPos;

    // Start is called before the first frame update
    void Start()
    {
        lastIdle = Idle;

        if (Idle)
        {
            StartIdle();
        }

        StartCoroutine(ChooseTargets());
    }

    // Update is called once per frame
    void Update()
    {
        if (lastIdle != Idle)
        {
            if (Idle)
            {
                StartIdle();
            }

            lastIdle = Idle;
        }

        if (Idle)
        {
            Vector3 vel = Vector3.zero;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, 0.1f);

            transform.position = newPos;
        }
    }

    private void StartIdle()
    {
        centerPos = transform.position;
    }

    private IEnumerator ChooseTargets()
    {
        while (true)
        {
            targetPosition = Random.insideUnitSphere * IdleRange + centerPos;
            yield return new WaitForSeconds(TargetIntervall);
        }
    }
}

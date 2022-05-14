using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRSnapTurn : MonoBehaviour
{

    public InputActionReference TurnAxis;
    public float HighSensitivity = 0.7f;
    public float LowSensitivity = 0.2f;
    public float TimeSensitivity = 0.5f;
    public float TurnAmount = 90;

    private bool monitoring = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = TurnAxis.action.ReadValue<Vector2>().x;
        if (Mathf.Abs(value) > HighSensitivity && !monitoring)
        {
            StartCoroutine(MonitorTurn(value));
        } 
    }

    IEnumerator MonitorTurn(float value)
    {
        monitoring = true;
        bool direction = value > 0;

        float timer = 0;
        bool turn = false;
        while (true)
        {
            value = TurnAxis.action.ReadValue<Vector2>().x;
            if (Mathf.Abs(value) < LowSensitivity && timer < TimeSensitivity)
            {
                turn = true;
                break;
            }

            if (timer > TimeSensitivity) break;

            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        if (turn)
        {
            transform.Rotate(Vector3.up * TurnAmount * (direction ? 1 : -1));
        }

        monitoring = false;
    }
}

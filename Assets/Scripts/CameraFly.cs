using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFly : PortalTraveller
{

    public float Sensitivity = 5;
    public float Speed = 2;

    private float pitch;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pitch = transform.localEulerAngles.x;

        rb = GetComponent<Rigidbody>();
    }

    private float updown = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        float multiplier = 1;
        if (Input.GetKey(KeyCode.LeftShift)) multiplier = 2;

        // Translation
        Vector3 dir = Vector3.zero;
        dir += transform.right * Input.GetAxis("Horizontal");
        dir += transform.forward * Input.GetAxis("Vertical");

        float targetUpDown = 0;
        if (Input.GetKey(KeyCode.Q)) targetUpDown = -1;
        if (Input.GetKey(KeyCode.E)) targetUpDown = 1;
        updown = Mathf.Lerp(updown, targetUpDown, 0.1f);
        dir += Vector3.up * updown;

        //transform.Translate(dir * Speed * multiplier * Time.deltaTime, Space.Self);
        rb.velocity = dir * Speed * multiplier;

        // Rotation
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (mouseDelta.magnitude > 5) mouseDelta = Vector3.zero;

        pitch += mouseDelta.y;
        //if (pitch > 80 || pitch < -80) mouseDelta.y = 0;

        Vector3 rot = transform.rotation.eulerAngles;
        rot.x += -mouseDelta.y * Sensitivity;
        rot.y += mouseDelta.x * Sensitivity;
        transform.rotation = Quaternion.Euler(rot);
    }
}

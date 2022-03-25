using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PortalTraveller
{

    public float MouseSensitivity = 5;
    public float MovementSpeed = 2;
    public float JumpForce = 5;
    public LayerMask GroundLayer;

    public AnimSpeedTest AnimSpeedTest;

    private float pitch;

    private Rigidbody rb;
    private Camera cam;
    private Collider collider;

    [SerializeField] private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pitch = transform.localEulerAngles.x;

        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        collider = GetComponent<Collider>();
    }

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

        Vector3 vel = dir * MovementSpeed * multiplier;
        rb.velocity = new Vector3(vel.x, vel.y + rb.velocity.y, vel.z);

        // Rotation
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (mouseDelta.magnitude > 5) mouseDelta = Vector3.zero;

        pitch += mouseDelta.y * MouseSensitivity;
        if (pitch > 80 || pitch < -80) mouseDelta.y = 0;

        transform.Rotate(Vector3.up, mouseDelta.x * MouseSensitivity);
        cam.transform.Rotate(Vector3.right, -mouseDelta.y * MouseSensitivity);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        // Other
        AnimSpeedTest.AnimationSpeed = dir.magnitude * multiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer & GroundLayer.value) == 0) isGrounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.layer & GroundLayer.value) == 0) isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((collision.gameObject.layer & GroundLayer.value) == 0) isGrounded = false;
    }
}

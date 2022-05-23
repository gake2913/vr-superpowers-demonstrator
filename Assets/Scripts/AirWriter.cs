using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWriter : MonoBehaviour
{

    public GameObject WritingParentPrefab;
    public float WritingDistance = 1;
    public Transform RedRoomRoot;
    public float Thickness = 0.05f;
    public float ThicknessMin = 0.02f;
    public float ThicknessMax = 0.2f;
    public float ThicknessStep = 0.01f;
    public bool WritingActive = false;

    private Transform currentParent;

    private bool lastWritingActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - RedRoomRoot.position.x) > 10) return;
        if (Mathf.Abs(transform.position.y - RedRoomRoot.position.y) > 10) return;
        if (Mathf.Abs(transform.position.z - RedRoomRoot.position.z) > 10) return;

        if (Input.GetMouseButtonDown(1) || (WritingActive && !lastWritingActive))
        {
            GameObject parent = Instantiate(WritingParentPrefab);
            parent.transform.position = transform.position + transform.forward * WritingDistance;
            currentParent = parent.transform;
        }

        if (Input.GetMouseButton(1) || WritingActive)
        {
            currentParent.position = transform.position + transform.forward * WritingDistance;

            float scrollY = 0;// Input.mouseScrollDelta.y;
            Thickness += scrollY * ThicknessStep;
            Thickness = Mathf.Clamp(Thickness, ThicknessMin, ThicknessMax);
            currentParent.GetComponent<AirWriterControl>().UpdateThickness(Thickness);
        }

        if (Input.GetMouseButtonUp(1) || (!WritingActive && lastWritingActive))
        {
            currentParent = null;
        }

        lastWritingActive = WritingActive;
    }
}

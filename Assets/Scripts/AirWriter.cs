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

    private Transform currentParent;

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

        if (Input.GetMouseButtonDown(1))
        {
            GameObject parent = Instantiate(WritingParentPrefab);
            parent.transform.position = Camera.main.transform.position + Camera.main.transform.forward * WritingDistance;
            currentParent = parent.transform;
        }

        if (Input.GetMouseButton(1))
        {
            currentParent.position = Camera.main.transform.position + Camera.main.transform.forward * WritingDistance;

            float scrollY = Input.mouseScrollDelta.y;
            Thickness += scrollY * ThicknessStep;
            Thickness = Mathf.Clamp(Thickness, ThicknessMin, ThicknessMax);
            currentParent.GetComponent<AirWriterControl>().UpdateThickness(Thickness);
        }

        if (Input.GetMouseButtonUp(1))
        {
            currentParent = null;
        }
    }
}

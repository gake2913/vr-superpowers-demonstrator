using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostVision : MonoBehaviour
{

    public LayerMask GhostVisionLayer;

    private new Camera camera;
    private int originalCullingMask;
    private int ghostCullingMask;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        originalCullingMask = camera.cullingMask;
        ghostCullingMask = originalCullingMask | GhostVisionLayer.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            camera.cullingMask = ghostCullingMask;
        }
        else
        {
            camera.cullingMask = originalCullingMask;
        }
    }
}

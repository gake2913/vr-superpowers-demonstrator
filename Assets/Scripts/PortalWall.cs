using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalWall : MonoBehaviour
{

    public MeshCollider PortalScreen;

    // Start is called before the first frame update
    void Start()
    {
        GameObject child = new GameObject("PortalWall Mesh");
        child.transform.parent = transform;
        child.transform.localPosition = Vector3.zero;
        child.transform.localRotation = Quaternion.identity;
        child.transform.localScale = Vector3.one;
        MeshFilter filter = child.AddComponent<MeshFilter>();
        MeshRenderer mr = child.AddComponent<MeshRenderer>();
        mr.materials = GetComponent<MeshRenderer>().materials;

        if (!TryGetComponent<MeshCollider>(out MeshCollider selfR))
        {
            gameObject.AddComponent<MeshCollider>();
        }
        MeshCollider self = gameObject.GetComponent<MeshCollider>();
        BooleanMesh booleanMesh = new BooleanMesh(self, PortalScreen);
        filter.mesh = booleanMesh.Difference();

        self.enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        child.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
